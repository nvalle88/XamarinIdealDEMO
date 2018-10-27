using System;
using System.Collections.Generic;
using System.Text;
using App1.Models;

namespace App1.ViewModels
{
    class TranslationViewModel
    {
        public Translations Translations { get; set; }

        public TranslationViewModel(Translations translations)
        {
            Translations = translations;
        }
    }
}
