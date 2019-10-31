using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GineCore.Entity.Entities
{
    public partial class Menus : BaseEntity
    {

        [Key]
        [Column("Id")]
        public int Id { get; set; }

        [Column("Icon")]
        public string Icon { get; set; }

        [Column("LinkUrl")]
        public string LinkUrl { get; set; }

        [Column("Type")]
        public string Type { get; set; }

        [Column("Name")]
        public string Name { get; set; }

        [Column("Sort")]
        public int Sort { get; set; }

        [Column("ParentId")]
        public int? ParentId { get; set; }

        [Column("EnalbleMarked")]
        public bool EnableMarked { get; set; }

        [Column("Key")]
        public string Key { get; set; }
    }
}
