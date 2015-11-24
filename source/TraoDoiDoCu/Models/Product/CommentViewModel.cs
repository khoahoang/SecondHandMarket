using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TraoDoiDoCu.Models
{
    [MetadataTypeAttribute(typeof(CommentViewModel))]
    public partial class Comments
    {
        internal sealed class CommentViewModel
        {
            [Display(Name = "Comment ID")]//Thuộc tính Display dùng để đặt tên lại cho cột
            public int ID { get; set; }

            [Display(Name = "Product ID")]
            public int ProductID { get; set; }

            [Display(Name = "User ID ")]
            public int UserID { get; set; }

            [Display(Name = "Comment Content")]
            [StringLength(50)]
            public string TextContent { get; set; }

            [Display(Name = "PostingDate")]
            [DataType(DataType.Date)]
            [DisplayFormat(DataFormatString = "{0:g}", ApplyFormatInEditMode = true)]
            public System.DateTime PostingDate { get; set; }
        }
    }
}