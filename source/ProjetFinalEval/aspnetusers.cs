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
    
    public partial class aspnetusers
    {
        public aspnetusers()
        {
            this.aspnetuserclaims = new HashSet<aspnetuserclaims>();
            this.aspnetuserlogins = new HashSet<aspnetuserlogins>();
            this.collaborateurtitulaire = new HashSet<collaborateurtitulaire>();
            this.collaborateurpe = new HashSet<collaborateurpe>();
            this.aspnetroles = new HashSet<aspnetroles>();
        }
    
        public string Id { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public Nullable<System.DateTime> LockoutEndDateUtc { get; set; }
        public bool LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }
        public string UserName { get; set; }
    
        public virtual ICollection<aspnetuserclaims> aspnetuserclaims { get; set; }
        public virtual ICollection<aspnetuserlogins> aspnetuserlogins { get; set; }
        public virtual ICollection<collaborateurtitulaire> collaborateurtitulaire { get; set; }
        public virtual ICollection<collaborateurpe> collaborateurpe { get; set; }
        public virtual ICollection<aspnetroles> aspnetroles { get; set; }
    }
}
