//using Riok.Mapperly.Abstractions;
//using WebAPI.Models.DTO;

//namespace WebAPI.Models.Mapper
//{
//    [Mapper]
//    public partial class ReceiptMapper
//    {
//        [MapperIgnoreSource("Id")]
//        [MapperIgnoreSource("Vendor")]
//        [MapperIgnoreSource("Currency")]
//        public partial ReceiptDto ReceiptToReceiptDto(Receipt receipt);


//        [MapperIgnoreTarget("Id")]
//        [MapperIgnoreTarget("Vendor")]
//        [MapperIgnoreTarget("Currency")]
//        public partial Receipt ReceiptDtoToReceipt(ReceiptDto receiptDto);


//        [MapperIgnoreTarget("Vendor")]
//        [MapperIgnoreTarget("Currency")]
//        public partial Receipt ReceiptUpdateDtoToReceipt(ReceiptUpdateDto receiptDto);
//    }

//    [Mapper]
//    public partial class ReceiptLineMapper
//    {
//        [MapperIgnoreSource("Id")]
//        [MapperIgnoreSource("Receipt")]
//        [MapperIgnoreSource("Product")]
//        [MapperIgnoreSource("UnitOfMeasure")]
//        public partial ReceiptLineDto ReceiptLineToReceiptLineDto(ReceiptLine receiptLine);

//        [MapperIgnoreTarget("Id")]
//        [MapperIgnoreTarget("Receipt")]
//        [MapperIgnoreTarget("Product")]
//        [MapperIgnoreTarget("UnitOfMeasure")]
//        public partial ReceiptLine ReceiptLineDtoToReceipt(ReceiptLineDto receiptLineDto);
//    }
//}
