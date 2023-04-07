using FakturaMVC.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace FakturaMVC.Models
{
    [Table("Fakture")]
    public class Faktura
    {
        [Key]
        public int FakturaId { get; set; }

        [DisplayName("Broj fakture")]
        [MaxLength(15)]
        public string BrojFakture { get; set; } //Faktura br. 231-8/19 - "Broj-Mjesec/Godina"

        [DisplayName("Datum stvaranja")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DatumStvaranja { get; set; }

        [DisplayName("Datum dospijeća")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [ProvjeriDatumDospijeca]
        public DateTime DatumDospijeca { get; set; }

        [DisplayName("Ukupna cijena bez poreza")]
        public decimal UkupnaCijenaBezPoreza { get; set; }

        [DisplayName("Ukupna cijena s porezom")]
        public decimal UkupnaCijenaSPorezom { get; set; }

        [DisplayName("Stvaratelj računa")]
        public string StvarateljRacuna { get; set; }

        [DisplayName("Primatelj računa")]
        public string NazivPrimateljaRacuna { get; set; }

        [DefaultValue("false")]
        public bool Stornirano { get; set; }

        public virtual List<FakturaStavka> StavkeList { get; set; }

        [NotMapped]
        public List<SelectListItem> StavkeSelectList { get; set; }
    }   

    [Table("Stavke")]
    public class Stavka
    {
        [Key]
        public int StavkaId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Naziv stavke je obavezan")]
        [MaxLength(50)]
        public string Naziv { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Cijena stavke je obavezna")]
        [DisplayFormat(DataFormatString = "{0:N}", ApplyFormatInEditMode = true)]        
        public decimal Cijena { get; set; }

        [DefaultValue("false")]
        public bool Izbrisano { get; set; }
    }

    [Table("FakturaStavke")]
    public class FakturaStavka
    {
        [Key]
        public int FakturaStavkaId { get; set; }        
        public int FakturaId { get; set; }       
        public int StavkaId { get; set; }
        public string Opis { get; set; }
        public decimal Cijena { get; set; }
        public int Kolicina { get; set; }
        public decimal Porez { get; set; }

        [DisplayName("Ukupna cijena bez poreza")]
        public decimal UkupnaCijenaBezPoreza { get; set; }
        public virtual Stavka Stavka { get; set; }
        public virtual Faktura Faktura { get; set; }
    }  
}