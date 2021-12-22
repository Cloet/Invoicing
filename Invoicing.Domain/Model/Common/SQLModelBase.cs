using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Invoicing.Domain.Model.Common
{
    public abstract class SQLModelBase<T> : ModelBase<T>, ISQLObject, IEquatable<T>, IValidatableObject
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual int Id { get; set; }

        public bool IsNew() => Id <= 0;

        public static T Empty => default;

        public bool EqualsEmptyInstance()
        {
            return GetHashCode() == Empty.GetHashCode();
        }

        public bool Equals(T other)
        {
            return GetHashCode() == other.GetHashCode();
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public virtual IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            return null;
        }

    }
}
