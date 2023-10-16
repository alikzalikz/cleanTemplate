using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CharchoobApi.Domain.Entities.gnr;

[Table("tblRefreshToken", Schema = "gnr")]
public class TblRefreshToken
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(1000)]
    public required string JwtId { get; set; }

    [Required]
    [StringLength(450)]
    public required string UserId { get; set; }

    [Required]
    [StringLength(1000)]
    public required string Token { get; set; }

    public bool IsUsed { get; set; } = false;

    public bool IsRevoked { get; set; } = false;

    [Required]
    public required DateTime CreateDate { get; set; }

    [Required]
    public required DateTime ExpiryDate { get; set; }
}
