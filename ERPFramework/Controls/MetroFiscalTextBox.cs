using System;
using System.ComponentModel;
using System.Windows.Forms;
using MetroFramework;
using MetroFramework.Extender;

namespace ERPFramework.Controls
{
    public class MetroFiscalTextBox : MetroMaskedTextbox
    {
        public MetroFiscalTextBox()
            : base()
        {
        }

        private bool isChanged;

        #region Properties

        [Category(ExtenderDefaults.PropertyCategory.Behaviour)]
        public bool OnlyTaxNo { get; set; }

        #endregion

        protected override void OnTextChanged(EventArgs e)
        {
            isChanged = true;
            base.OnTextChanged(e);
        }

        protected override void OnValidating(System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = !CheckIsOK();
            base.OnValidating(e);
        }

        public bool CheckIsOK()
        {
            if (!isChanged)
                return true;

            switch (Text.Length)
            {
                case 0:
                    return true;
                case 11:
                    if (!TaxNoChecker.CheckTaxNo(Text))
                    {
                        if (MetroMessageBox.Show(FindForm(), Properties.Resources.Msg_TaxNo + Properties.Resources.Msg_Continue,
                                                  Properties.Resources.Warning,
                                                  MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                            return false;
                    }
                    break;

                case 16:
                    if (OnlyTaxNo)
                        return false;

                    if (!FiscalCodeChecker.CheckFiscalCode(Text))
                    {
                        
                        if (MetroMessageBox.Show(FindForm(), Properties.Resources.Msg_FiscalCode + Properties.Resources.Msg_Continue,
                                                  Properties.Resources.Warning, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                            return false;
                    }
                    break;

                default:
                    if (MetroMessageBox.Show(FindForm(), Properties.Resources.Msg_LenIncorrect + Properties.Resources.Msg_Continue,
                                              Properties.Resources.Warning, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                        return false;
                    break;
            }
            isChanged = false;
            return true;
        }

        #region Internal Class

        private static class TaxNoChecker
        {
            private static int[] ListaPari = { 0, 2, 4, 6, 8, 1, 3, 5, 7, 9 };

            public static bool CheckTaxNo(string PartitaIva)
            {
                if (PartitaIva.Equals(string.Empty))
                    return true;

                // normalizziamo la cifra
                if (PartitaIva.Length < 11)
                    PartitaIva = PartitaIva.PadLeft(11, '0');

                // lunghezza errata non fa neanche il controllo
                if (PartitaIva.Length != 11)
                    return false;
                int Somma = 0;
                for (int k = 0; k < 11; k++)
                {
                    string s = PartitaIva.Substring(k, 1);

                    // otteniamo contemporaneamente
                    // il valore, la posizione e testiamo se ci sono
                    // caratteri non numerici
                    int i = "0123456789".IndexOf(s);
                    if (i == -1)
                        return false;
                    int x = int.Parse(s);
                    if (k % 2 == 1) // Pari perchè iniziamo da zero
                        x = ListaPari[i];
                    Somma += x;
                }
                return ((Somma % 10 == 0) && (Somma != 0));
            }
        }

        private static class FiscalCodeChecker
        {
            public static bool CheckFiscalCode(string CodiceFiscale)
            {
                bool result = false;
                const int caratteri = 16;
                if (CodiceFiscale == null || CodiceFiscale.Equals(string.Empty))
                    return true;

                if (CodiceFiscale.Length < caratteri)
                    return TaxNoChecker.CheckTaxNo(CodiceFiscale);

                // se il codice fiscale non è di 16 caratteri il controllo
                // è già finito prima ancora di cominciare

                if (CodiceFiscale.Length != caratteri)
                    return result;

                // stringa per controllo e calcolo omocodia
                const string omocodici = "LMNPQRSTUV";

                // per il calcolo del check digit e la conversione in numero
                const string listaControllo = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

                int[] listaPari = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25 };
                int[] listaDispari = { 1, 0, 5, 7, 9, 13, 15, 17, 19, 21, 2, 4, 18, 20, 11, 3, 6, 8, 12, 14, 16, 10, 22, 25, 24, 23 };

                CodiceFiscale = CodiceFiscale.ToUpper();
                char[] cCodice = CodiceFiscale.ToCharArray();

                // check della correttezza formale del codice fiscale
                // elimino dalla stringa gli eventuali caratteri utilizzati negli
                // spazi riservati ai 7 che sono diventati carattere in caso di omocodia
                for (int k = 6; k < 15; k++)
                {
                    if ((k == 8) || (k == 11))
                        continue;
                    int x = (omocodici.IndexOf(cCodice[k]));
                    if (x != -1)
                        cCodice[k] = x.ToString().ToCharArray()[0];
                }

                //  Regex rgx = new Regex(@"^[A-Z]{6}[]{2}[A-Z][]{2}[A-Z][]{3}[A-Z]$");
                //  Match m = rgx.Match(new string(cCodice));
                //  result = m.Success;
                result = true;
                // da una verifica ho trovato 3 risultati errati su più di 4000  codici fiscali
                // ho temporaneamente rimosso il test con le Regular fino a quando non riuscirò a capire perchè in alcuni casi sbaglia

                // normalizzato il codice fiscale se la regular non ha buon
                // fine è inutile continuare
                if (result)
                {
                    int somma = 0;

                    // ripristino il codice fiscale originario
                    // grazie a Lino Barreca che mi ha segnalato l'errore
                    cCodice = CodiceFiscale.ToCharArray();
                    for (int i = 0; i < 15; i++)
                    {
                        char c = cCodice[i];
                        int x = "0123456789".IndexOf(c);
                        if (x != -1)
                            c = listaControllo.Substring(x, 1).ToCharArray()[0];
                        x = listaControllo.IndexOf(c);

                        // ho inserito un carattere non valido
                        if (x < 0) return false;

                        // i modulo 2 = 0 è dispari perchè iniziamo da 0
                        if ((i % 2) == 0)
                            x = listaDispari[x];
                        else
                            x = listaPari[x];
                        somma += x;
                    }

                    result = (listaControllo.Substring(somma % 26, 1) == CodiceFiscale.Substring(15, 1));
                }
                return result;
            }
        }

        #endregion
    }
}
