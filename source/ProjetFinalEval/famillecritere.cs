//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré à partir d'un modèle.
//
//     Des modifications manuelles apportées à ce fichier peuvent conduire à un comportement inattendu de votre application.
//     Les modifications manuelles apportées à ce fichier sont remplacées si le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ProjetFinalEval
{
    using System;
    using System.Collections.Generic;
    
    public partial class famillecritere
    {
        public famillecritere()
        {
            this.critere = new HashSet<critere>();
        }
    
        public int IDFAMILLECRITERE { get; set; }
        public string NOMFAMILLECRITERE { get; set; }
    
        public virtual ICollection<critere> critere { get; set; }
    }
}
