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
    
    public partial class tache
    {
        public int IDTACHE { get; set; }
        public int IDCOLLABORATEURTITULAIRE { get; set; }
        public int IDPROJET { get; set; }
        public string NOMTACHE { get; set; }
        public Nullable<System.DateTime> DATEDEBUTTACHE { get; set; }
        public Nullable<System.DateTime> DATEFINTACHE { get; set; }
    
        public virtual collaborateurtitulaire collaborateurtitulaire { get; set; }
        public virtual projet projet { get; set; }
    }
}
