using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AddressBookApp.Models
{
    public enum Prefectures
    {
        北海道,
        青森県,
        岩手県,
        宮城県,
        秋田県,
        山形県,
        福島県,
        東京都,
        神奈川県,
        埼玉県,
        千葉県,
        茨城県,
        栃木県,
        群馬県,
        山梨県,
        新潟県,
        長野県,
        富山県,
        石川県,
        福井県,
        愛知県,
        岐阜県,
        静岡県,
        三重県,
        大阪府,
        兵庫県,
        京都府,
        滋賀県,
        奈良県,
        和歌山県,
        鳥取県,
        島根県,
        岡山県,
        広島県,
        山口県,
        徳島県,
        香川県,
        愛媛県,
        高知県,
        福岡県,
        佐賀県,
        長崎県,
        熊本県,
        大分県,
        宮崎県,
        鹿児島県,
        沖縄県
    }
    [MetadataType(typeof(AddressMetadata))]
    public partial class Address
    {
        [DisplayName("都道府県")]
        public Prefectures? PrefectureItem { get; set; }
    }
    public class AddressMetadata
    {
        [DisplayName("氏名")]
        [Required]
        public string Name { get; set; }

        [DisplayName("カナ")]
        [Required]
        public string Kana { get; set; }

        [DisplayName("郵便番号")]
        [RegularExpression(@"[0-9]+")]
        [StringLength(7)]
        public string ZipCode { get; set; }

        [DisplayName("都道府県")]
        public string Prefecture { get; set; }

        [DisplayName("住所")]
        public string StreetAddress { get; set; }

        [DisplayName("電話番号")]
        [RegularExpression(@"[0-9]+", ErrorMessage ="Please enter valid telephone")]
        [StringLength(11)]
        public string Telephone { get; set; }

        [DisplayName("メール")]
        [DataType(DataType.EmailAddress)]
        public string Mail { get; set; }

        [DisplayName("グループ")]
        public Nullable<int> Group_Id { get; set; }
    }
}