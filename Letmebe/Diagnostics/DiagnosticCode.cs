namespace Letmebe.Diagnostics {
    public enum DiagnosticCode {
        /// <summary>
        /// Unexpected character
        /// </summary>
        LMB0001,

        /// <summary>
        /// Unexpected token
        /// </summary>
        LMB0002,

        /// <summary>
        /// Expected expression
        /// </summary>
        LMB0003,

        /// <summary>
        /// Expected statement after if
        /// </summary>
        LMB0004,

        /// <summary>
        /// Expected statement after if-otherwise
        /// </summary>
        LMB0005,

        /// <summary>
        /// Expected statement after unless
        /// </summary>
        LMB0006,

        /// <summary>
        /// Expected statement after forever
        /// </summary>
        LMB0007,

        /// <summary>
        /// Expected statement after repeat-times
        /// </summary>
        LMB0008,

        /// <summary>
        /// Expected statement after while
        /// </summary>
        LMB0009,

        /// <summary>
        /// Expected statement after until
        /// </summary>
        LMB0010,

        /// <summary>
        /// Expected statement after do
        /// </summary>
        LMB0011,

        /// <summary>
        /// Expected while or until after do
        /// </summary>
        LMB0012,

        /// <summary>
        /// Expected statement after function definition
        /// </summary>
        LMB0013,

        /// <summary>
        /// Cannot use block statement without control flow statement
        /// </summary>
        LMB0014,

        /// <summary>
        /// Expected type expression
        /// </summary>
        LMB0015,

        /// <summary>
        /// Expected sentence or identifier
        /// </summary>
        LMB0016,

        /// <summary>
        /// Undefined type in current scope
        /// </summary>
        LMB0017,

        /// <summary>
        /// Undefined generic type in current scope
        /// </summary>
        LMB0018,

        /// <summary>
        /// Variable does not exist in current scope
        /// </summary>
        LMB0019,

        /// <summary>
        /// Binary operator does not exist between types
        /// </summary>
        LMB0020,

        /// <summary>
        /// Unary operator does not exist on type
        /// </summary>
        LMB0021,

        /// <summary>
        /// Indexer operator does not exist
        /// </summary>
        LMB0022,

        /// <summary>
        /// Function does not exist
        /// </summary>
        LMB0023,

        /// <summary>
        /// Variable already exists
        /// </summary>
        LMB0024,

        /// <summary>
        /// Repeat-times amount must be integer
        /// </summary>
        LMB0025,

        /// <summary>
        /// Do-while condition must be boolean
        /// </summary>
        LMB0026,

        /// <summary>
        /// Do-until condition must be boolean
        /// </summary>
        LMB0027,

        /// <summary>
        /// If condition must be boolean
        /// </summary>
        LMB0028,

        /// <summary>
        /// Unless condition must be boolean
        /// </summary>
        LMB0029,

        /// <summary>
        /// If-otherwise condition must be boolean
        /// </summary>
        LMB0030,

        /// <summary>
        /// While condition must be boolean
        /// </summary>
        LMB0031,

        /// <summary>
        /// Until condition must be boolean
        /// </summary>
        LMB0032,

        /// <summary>
        /// Function signature must have identifier
        /// </summary>
        LMB0033,

        /// <summary>
        /// Function parameter already declared
        /// </summary>
        LMB0034,

        /// <summary>
        /// Function already defined in current scope
        /// </summary>
        LMB0035,

        /// <summary>
        /// Return statement must be used within a function
        /// </summary>
        LMB0036,

        /// <summary>
        /// Current function is expected to return given type
        /// </summary>
        LMB0037,

        /// <summary>
        /// Cannot assign type to target type
        /// </summary>
        LMB0038,

        /// <summary>
        /// Expression statement must be assignment or function call
        /// </summary>
        LMB0039,

        /// <summary>
        /// Array must be indexed with integer
        /// </summary>
        LMB0040,
    }
}
