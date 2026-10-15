CREATE TABLE [Admissions].[ComplementaryAdmissions] (
    [id]     BIGINT        IDENTITY (1, 1) NOT NULL,
    [code]   VARCHAR (3)   NOT NULL,
    [names]  VARCHAR (100) NOT NULL,
    [estado] BIT           NOT NULL,
    CONSTRAINT [PK__Compleme__3213E83F33681CE3] PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [UK_codigo] UNIQUE NONCLUSTERED ([code] ASC)
);

