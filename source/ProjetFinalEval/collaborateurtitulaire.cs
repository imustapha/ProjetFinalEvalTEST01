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
    using System.Web;
    
    public partial class collaborateurtitulaire
    {
        public collaborateurtitulaire()
        {
            this.projet = new HashSet<projet>();
            this.evaluation = new HashSet<evaluation>();
            this.tache = new HashSet<tache>();
            this.variable = new HashSet<variable>();
        }
    
        public int IDCOLLABORATEURTITULAIRE { get; set; }
        public int IDFONCTION { get; set; }
        public string NOM { get; set; }
        public string PRENOM { get; set; }
        public byte[] IMAGE { get; set; }
        public bool FLAGEVAL { get; set; }
        public string IdUser { get; set; }
        public HttpPostedFileBase File { get; set; }
    
        public virtual aspnetusers aspnetusers { get; set; }
        public virtual ICollection<projet> projet { get; set; }
        public virtual fonction fonction { get; set; }
        public virtual ICollection<evaluation> evaluation { get; set; }
        public virtual ICollection<tache> tache { get; set; }
        public virtual ICollection<variable> variable { get; set; }
    }
}
