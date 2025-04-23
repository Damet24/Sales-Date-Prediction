namespace Infrastructure;

public static class DatabaseErrors
{
    public const int ViolationOfConstraint = 2627;
    public const int ConflictedWithTheConstraint= 547;
    public const int CannotInsertDuplicateKeyRow = 2601;
}