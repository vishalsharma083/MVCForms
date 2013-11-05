﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.ComponentModel;
using System.Data.EntityClient;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using System.Linq;
using System.Runtime.Serialization;
using System.Xml.Serialization;

[assembly: EdmSchemaAttribute()]
#region EDM Relationship Metadata

[assembly: EdmRelationshipAttribute("MVCFormsResponseModel", "FK_FormResponseItems_FormResponses", "FormResponses", System.Data.Metadata.Edm.RelationshipMultiplicity.One, typeof(MvcFormsV2.Models.FormResponse), "FormResponseItems", System.Data.Metadata.Edm.RelationshipMultiplicity.Many, typeof(MvcFormsV2.Models.FormResponseItem), true)]

#endregion

namespace MvcFormsV2.Models
{
    #region Contexts
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    public partial class MVCFormsResponseEntities : ObjectContext
    {
        #region Constructors
    
        /// <summary>
        /// Initializes a new MVCFormsResponseEntities object using the connection string found in the 'MVCFormsResponseEntities' section of the application configuration file.
        /// </summary>
        public MVCFormsResponseEntities() : base("name=MVCFormsResponseEntities", "MVCFormsResponseEntities")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        /// <summary>
        /// Initialize a new MVCFormsResponseEntities object.
        /// </summary>
        public MVCFormsResponseEntities(string connectionString) : base(connectionString, "MVCFormsResponseEntities")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        /// <summary>
        /// Initialize a new MVCFormsResponseEntities object.
        /// </summary>
        public MVCFormsResponseEntities(EntityConnection connection) : base(connection, "MVCFormsResponseEntities")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        #endregion
    
        #region Partial Methods
    
        partial void OnContextCreated();
    
        #endregion
    
        #region ObjectSet Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public ObjectSet<FormResponseItem> FormResponseItems
        {
            get
            {
                if ((_FormResponseItems == null))
                {
                    _FormResponseItems = base.CreateObjectSet<FormResponseItem>("FormResponseItems");
                }
                return _FormResponseItems;
            }
        }
        private ObjectSet<FormResponseItem> _FormResponseItems;
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public ObjectSet<FormResponse> FormResponses
        {
            get
            {
                if ((_FormResponses == null))
                {
                    _FormResponses = base.CreateObjectSet<FormResponse>("FormResponses");
                }
                return _FormResponses;
            }
        }
        private ObjectSet<FormResponse> _FormResponses;

        #endregion

        #region AddTo Methods
    
        /// <summary>
        /// Deprecated Method for adding a new object to the FormResponseItems EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
        /// </summary>
        public void AddToFormResponseItems(FormResponseItem formResponseItem)
        {
            base.AddObject("FormResponseItems", formResponseItem);
        }
    
        /// <summary>
        /// Deprecated Method for adding a new object to the FormResponses EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
        /// </summary>
        public void AddToFormResponses(FormResponse formResponse)
        {
            base.AddObject("FormResponses", formResponse);
        }

        #endregion

    }

    #endregion

    #region Entities
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    [EdmEntityTypeAttribute(NamespaceName="MVCFormsResponseModel", Name="FormResponse")]
    [Serializable()]
    [DataContractAttribute(IsReference=true)]
    public partial class FormResponse : EntityObject
    {
        #region Factory Method
    
        /// <summary>
        /// Create a new FormResponse object.
        /// </summary>
        /// <param name="uid">Initial value of the Uid property.</param>
        /// <param name="formUid">Initial value of the FormUid property.</param>
        /// <param name="timestamp">Initial value of the Timestamp property.</param>
        public static FormResponse CreateFormResponse(global::System.Guid uid, global::System.Guid formUid, global::System.DateTime timestamp)
        {
            FormResponse formResponse = new FormResponse();
            formResponse.Uid = uid;
            formResponse.FormUid = formUid;
            formResponse.Timestamp = timestamp;
            return formResponse;
        }

        #endregion

        #region Primitive Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Guid Uid
        {
            get
            {
                return _Uid;
            }
            set
            {
                if (_Uid != value)
                {
                    OnUidChanging(value);
                    ReportPropertyChanging("Uid");
                    _Uid = StructuralObject.SetValidValue(value);
                    ReportPropertyChanged("Uid");
                    OnUidChanged();
                }
            }
        }
        private global::System.Guid _Uid;
        partial void OnUidChanging(global::System.Guid value);
        partial void OnUidChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Guid FormUid
        {
            get
            {
                return _FormUid;
            }
            set
            {
                OnFormUidChanging(value);
                ReportPropertyChanging("FormUid");
                _FormUid = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("FormUid");
                OnFormUidChanged();
            }
        }
        private global::System.Guid _FormUid;
        partial void OnFormUidChanging(global::System.Guid value);
        partial void OnFormUidChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.DateTime Timestamp
        {
            get
            {
                return _Timestamp;
            }
            set
            {
                OnTimestampChanging(value);
                ReportPropertyChanging("Timestamp");
                _Timestamp = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("Timestamp");
                OnTimestampChanged();
            }
        }
        private global::System.DateTime _Timestamp;
        partial void OnTimestampChanging(global::System.DateTime value);
        partial void OnTimestampChanged();

        #endregion

    
        #region Navigation Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [XmlIgnoreAttribute()]
        [SoapIgnoreAttribute()]
        [DataMemberAttribute()]
        [EdmRelationshipNavigationPropertyAttribute("MVCFormsResponseModel", "FK_FormResponseItems_FormResponses", "FormResponseItems")]
        public EntityCollection<FormResponseItem> FormResponseItems
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedCollection<FormResponseItem>("MVCFormsResponseModel.FK_FormResponseItems_FormResponses", "FormResponseItems");
            }
            set
            {
                if ((value != null))
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedCollection<FormResponseItem>("MVCFormsResponseModel.FK_FormResponseItems_FormResponses", "FormResponseItems", value);
                }
            }
        }

        #endregion

    }
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    [EdmEntityTypeAttribute(NamespaceName="MVCFormsResponseModel", Name="FormResponseItem")]
    [Serializable()]
    [DataContractAttribute(IsReference=true)]
    public partial class FormResponseItem : EntityObject
    {
        #region Factory Method
    
        /// <summary>
        /// Create a new FormResponseItem object.
        /// </summary>
        /// <param name="uid">Initial value of the Uid property.</param>
        /// <param name="formResponseUid">Initial value of the FormResponseUid property.</param>
        /// <param name="formItemUid">Initial value of the FormItemUid property.</param>
        /// <param name="timestamp">Initial value of the Timestamp property.</param>
        public static FormResponseItem CreateFormResponseItem(global::System.Guid uid, global::System.Guid formResponseUid, global::System.Guid formItemUid, global::System.DateTime timestamp)
        {
            FormResponseItem formResponseItem = new FormResponseItem();
            formResponseItem.Uid = uid;
            formResponseItem.FormResponseUid = formResponseUid;
            formResponseItem.FormItemUid = formItemUid;
            formResponseItem.Timestamp = timestamp;
            return formResponseItem;
        }

        #endregion

        #region Primitive Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Guid Uid
        {
            get
            {
                return _Uid;
            }
            set
            {
                if (_Uid != value)
                {
                    OnUidChanging(value);
                    ReportPropertyChanging("Uid");
                    _Uid = StructuralObject.SetValidValue(value);
                    ReportPropertyChanged("Uid");
                    OnUidChanged();
                }
            }
        }
        private global::System.Guid _Uid;
        partial void OnUidChanging(global::System.Guid value);
        partial void OnUidChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Guid FormResponseUid
        {
            get
            {
                return _FormResponseUid;
            }
            set
            {
                OnFormResponseUidChanging(value);
                ReportPropertyChanging("FormResponseUid");
                _FormResponseUid = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("FormResponseUid");
                OnFormResponseUidChanged();
            }
        }
        private global::System.Guid _FormResponseUid;
        partial void OnFormResponseUidChanging(global::System.Guid value);
        partial void OnFormResponseUidChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Guid FormItemUid
        {
            get
            {
                return _FormItemUid;
            }
            set
            {
                OnFormItemUidChanging(value);
                ReportPropertyChanging("FormItemUid");
                _FormItemUid = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("FormItemUid");
                OnFormItemUidChanged();
            }
        }
        private global::System.Guid _FormItemUid;
        partial void OnFormItemUidChanging(global::System.Guid value);
        partial void OnFormItemUidChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String ResponseStr
        {
            get
            {
                return _ResponseStr;
            }
            set
            {
                OnResponseStrChanging(value);
                ReportPropertyChanging("ResponseStr");
                _ResponseStr = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("ResponseStr");
                OnResponseStrChanged();
            }
        }
        private global::System.String _ResponseStr;
        partial void OnResponseStrChanging(global::System.String value);
        partial void OnResponseStrChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.DateTime Timestamp
        {
            get
            {
                return _Timestamp;
            }
            set
            {
                OnTimestampChanging(value);
                ReportPropertyChanging("Timestamp");
                _Timestamp = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("Timestamp");
                OnTimestampChanged();
            }
        }
        private global::System.DateTime _Timestamp;
        partial void OnTimestampChanging(global::System.DateTime value);
        partial void OnTimestampChanged();

        #endregion

    
        #region Navigation Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [XmlIgnoreAttribute()]
        [SoapIgnoreAttribute()]
        [DataMemberAttribute()]
        [EdmRelationshipNavigationPropertyAttribute("MVCFormsResponseModel", "FK_FormResponseItems_FormResponses", "FormResponses")]
        public FormResponse FormResponse
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<FormResponse>("MVCFormsResponseModel.FK_FormResponseItems_FormResponses", "FormResponses").Value;
            }
            set
            {
                ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<FormResponse>("MVCFormsResponseModel.FK_FormResponseItems_FormResponses", "FormResponses").Value = value;
            }
        }
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [BrowsableAttribute(false)]
        [DataMemberAttribute()]
        public EntityReference<FormResponse> FormResponseReference
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<FormResponse>("MVCFormsResponseModel.FK_FormResponseItems_FormResponses", "FormResponses");
            }
            set
            {
                if ((value != null))
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedReference<FormResponse>("MVCFormsResponseModel.FK_FormResponseItems_FormResponses", "FormResponses", value);
                }
            }
        }

        #endregion

    }

    #endregion

    
}
