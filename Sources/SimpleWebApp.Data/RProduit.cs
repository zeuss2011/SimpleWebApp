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
using System.Xml.Linq;
using System.Linq;
using System.Data.Entity;
using log4net;


namespace SimpleWebApp.Data
{
    public partial class RProduit : ARepository<Produit,int> , IRepository<Produit>
    {
    
    
    	#region Constructor(s)
    
    	public RProduit(SimpleWebAppEntities context) : base(context){}
    
    	#endregion
    
        #region Overrided Methods
    
        public override Produit GetById(int id)
        {
            return Single(id);
        }
    
        #endregion
    
    	#region Mapping
    
    	protected override void Map(Produit source, Produit target){
    		if(!Object.Equals(source.Nom,target.Nom)){
    			target.Nom = source.Nom;
    		}        
    		if(!Object.Equals(source.Prix,target.Prix)){
    			target.Prix = source.Prix;
    		}        
    	}
    
    	#endregion
    }
    }
