using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProblemD.Models
{
    public abstract class BaseEntity
    {
        [Key]
        public int Id {get;set;}
        public string Name {get;set;}
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreatedAt {get;set;}
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime UpdatedAt {get;set;}
    }
}