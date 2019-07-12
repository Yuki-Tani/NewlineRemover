using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Windows;

namespace NewlineRemover.Models
{
    public class Sentence
    {
        string content;

        public Sentence(string content)
        {
            this.content = content;
        }

        public Sentence RemoveNewline()
        {
            Regex target = new Regex(" *(\r\n|\r|\n) *");
            string removed_content = target.Replace(content, " ");
            return new Sentence(removed_content);
        }

        public void CopyToClipboard()
        {
            Clipboard.SetText(content);
        }

        public override string ToString()
        {
            return content;
        }
    }
}
