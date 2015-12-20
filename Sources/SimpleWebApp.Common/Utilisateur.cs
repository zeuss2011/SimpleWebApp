//------------------------------------------------------------------------------
// <auto-generated>
//    (c)Netapsys 2012
//    Ce code a ete genere par template T4.
//    Ne pas le modifier manuellement !
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using SimpleWebApp.Common;
using System.Runtime.Serialization;

namespace SimpleWebApp.Common
{
    
    [DataContract(Name = "Utilisateur", Namespace = "SimpleWebApp", IsReference = true)]
    
    public partial class Utilisateur : AEntity<int>
    {
    
        public Utilisateur()
        {
    		//this.Token = new List<TokenEntity>();
        }
    
    	[DataMember()]
        public string Login { get; set; }
    
    	[DataMember()]
        public string PassswordHash { get; set; }
    
    	[DataMember()]
        public string Nom { get; set; }
    
    	[DataMember()]
        public string Email { get; set; }
    
    	[DataMember()]
        public bool EmailConfirmed { get; set; }
    
    
    
    	[DataMember()]
        public virtual List<TokenEntity> Token 
    	{ 
    		get
    		{
    			if(_Token == null) _Token = new List<TokenEntity>();
    			return _Token;
    		}
    		set
    		{
    			_Token = value;
    		}
    	}
    	private List<TokenEntity> _Token;
    
    }
    
}
