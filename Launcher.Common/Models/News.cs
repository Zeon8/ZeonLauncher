using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace Launcher.Common.Models;
public class News
{
    [Key]
    public int Id { get; set; }

    public string Title { get; set; }

    [DataType(DataType.MultilineText)]
    public string Description { get; set; }

    public DateOnly Date { get; set; }

    [DisplayName("Image")]
    [Url]
    public string ImageUrl { get; set; }
}
