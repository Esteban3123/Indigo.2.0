CREATE TABLE [dbo].[ADCONTIPS] (
    [CODIGOIPS]   CHAR (100)                                                         NOT NULL,
    [DSCRIPIPS]   VARCHAR (50)                                                       NOT NULL,
    [ESTADO]      BIT                                                                NOT NULL,
    [CODIGONIT]   CHAR (15) MASKED WITH (FUNCTION = 'partial(0, "Nit_Ofuscado", 0)') NULL,
    [DIRECCIPS]   VARCHAR (100)                                                      NULL,
    [TELEFOIPS]   VARCHAR (15)                                                       NULL,
    [CORREOIPS]   VARCHAR (100)                                                      NULL,
    [DEPMUNCOD]   CHAR (5)                                                           NULL,
    [NIVATEIPS]   CHAR (1)                                                           NULL,
    [CLASEIPSS]   CHAR (1)                                                           NULL,
    [NOMREPLEG]   VARCHAR (60)                                                       NULL,
    [IDEREPLEG]   VARCHAR (15)                                                       NULL,
    [CODCENATE]   CHAR (10)                                                          NULL,
    [CODHABILITA] VARCHAR (15)                                                       NOT NULL,
    [DISTANCIA]   NUMERIC (18)                                                       NULL,
    [TIEMESPERA]  NUMERIC (18)                                                       NULL,
    CONSTRAINT [PK_ADCONTIPS] PRIMARY KEY CLUSTERED ([CODIGOIPS] ASC),
    CONSTRAINT [FK_ADINGRESO_ADCONTIPS] FOREIGN KEY ([CODIGOIPS]) REFERENCES [dbo].[ADCONTIPS] ([CODIGOIPS]),
    CONSTRAINT [FK_CODCENATE] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [CODHABILITA] UNIQUE NONCLUSTERED ([CODHABILITA] ASC)
);


GO
ALTER TABLE [dbo].[ADCONTIPS] NOCHECK CONSTRAINT [FK_ADINGRESO_ADCONTIPS];


GO
ALTER TABLE [dbo].[ADCONTIPS] NOCHECK CONSTRAINT [FK_CODCENATE];

