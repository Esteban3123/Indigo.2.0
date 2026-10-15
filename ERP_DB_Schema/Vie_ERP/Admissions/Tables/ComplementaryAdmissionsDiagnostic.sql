CREATE TABLE [Admissions].[ComplementaryAdmissionsDiagnostic] (
    [id]                        INT           IDENTITY (1, 1) NOT NULL,
    [code]                      VARCHAR (6)   NOT NULL,
    [diagnostico]               VARCHAR (200) NOT NULL,
    [IdComplementaryAdmissions] BIGINT        NULL,
    CONSTRAINT [PK__Compleme__3213E83F6BF560EA] PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [FK__Complemen__IdCom__5B166FB0] FOREIGN KEY ([IdComplementaryAdmissions]) REFERENCES [Admissions].[ComplementaryAdmissions] ([id]),
    CONSTRAINT [UQ__Compleme__357D4CF9A16C5F04] UNIQUE NONCLUSTERED ([code] ASC)
);

