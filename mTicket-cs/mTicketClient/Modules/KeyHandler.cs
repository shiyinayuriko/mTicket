using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using RawInput_dll;

namespace mTicketClient
{
    public class KeyHandler
    {
        private readonly StringBuilder _inputBuf;
        private bool _isShifted = false;
        public KeyHandler()
        {
            _inputBuf = new StringBuilder();
        }

        public string Pressed(KeyPressEvent keyPressEvent)
        {
            Keys key = (Keys) keyPressEvent.VKey;

            if (key.ToString().Contains("ShiftKey"))
            {
                if (keyPressEvent.KeyPressState.Equals("MAKE")) _isShifted = true;
                if (keyPressEvent.KeyPressState.Equals("BREAK")) _isShifted = false;
            }
            else if (key == Keys.Enter)
            {
                if (keyPressEvent.KeyPressState.Equals("MAKE"))
                {
                    var ret = _inputBuf.ToString();
                    _inputBuf.Length=0;
                    return ret;
                }
            }
            else if (keyPressEvent.KeyPressState.Equals("MAKE"))
            {
                char? c = GetKeyValue(key, _isShifted);
                _inputBuf.Append(c);
            }
            return null;
        }


        private char? GetKeyValue(Keys vkey,bool isShifted)
        {
            if ((int) vkey >= 65 && (int) vkey <= 90)
            {
                return isShifted ? (char) vkey : Char.ToLower((char) vkey);
            }
            switch (vkey)
            {
                case Keys.NumPad1: return '1';
                case Keys.NumPad2: return '2';
                case Keys.NumPad3: return '3';
                case Keys.NumPad4: return '4';
                case Keys.NumPad5: return '5';
                case Keys.NumPad6: return '6';
                case Keys.NumPad7: return '7';
                case Keys.NumPad8: return '8';
                case Keys.NumPad9: return '9';
                case Keys.NumPad0: return '0';
                case Keys.Add: return '+';
                case Keys.Subtract:return '-';
                case Keys.Multiply: return '*';
                case Keys.Divide: return '/';
                case Keys.Decimal: return '.';

                case Keys.Oemtilde: return isShifted ? '~' : '`';
                case Keys.D1: return isShifted ? '!' : '1';
                case Keys.D2: return isShifted ? '@' : '2';
                case Keys.D3: return isShifted ? '#' : '3';
                case Keys.D4: return isShifted ? '$' : '4';
                case Keys.D5: return isShifted ? '%' : '5';
                case Keys.D6: return isShifted ? '^' : '6';
                case Keys.D7: return isShifted ? '&' : '7';
                case Keys.D8: return isShifted ? '*' : '8';
                case Keys.D9: return isShifted ? '(' : '9';
                case Keys.D0: return isShifted ? ')' : '0';
                case Keys.OemMinus: return isShifted ? '_' : '-';
                case Keys.Oemplus: return isShifted ? '+' : '=';
                case Keys.OemPipe: return isShifted ? '|' : '/';

                case Keys.OemOpenBrackets: return isShifted ? '{' : '[';
                case Keys.OemCloseBrackets: return isShifted ? '}' : ']';
                case Keys.OemSemicolon: return isShifted ? ':' : ';';
                case Keys.OemQuotes: return isShifted ? '"' : '\'';
                case Keys.Oemcomma: return isShifted ? '<' : ',';
                case Keys.OemPeriod: return isShifted ? '>' : '.';
                case Keys.OemQuestion: return isShifted ? '?' : '/';

                default:
                    return null;
            }
        }
    }
}
