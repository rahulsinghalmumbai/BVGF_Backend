namespace BVGF.Entities
{
    public class CategoryMember: BaseEntity
    {
        public long CategoryMemberID { get; set; }

        public long CategoryID { get; set; }
        public long MemberID { get; set; }
    }
}
