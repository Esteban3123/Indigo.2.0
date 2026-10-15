CREATE TABLE [dbo].[NTVARIABLES] (
    [ID]              INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDNTGRUPO]       INT           NOT NULL,
    [VARIABLE]        VARCHAR (150) NOT NULL,
    [TIPO]            TINYINT       NOT NULL,
    [GENERO]          TINYINT       NOT NULL,
    [EDADMIN]         INT           NOT NULL,
    [EDADMAX]         INT           NOT NULL,
    [MANEJADECIMAL]   BIT           NULL,
    [CODSERIPS]       CHAR (20)     NULL,
    [OCULTOVERDADERO] BIT           NULL,
    [CODUSUCRE]       CHAR (20)     NOT NULL,
    [FECHACREA]       DATETIME      NOT NULL,
    [CODUSUMOD]       CHAR (20)     NULL,
    [FECHAMOD]        DATETIME      NULL,
    CONSTRAINT [PK_NTVARIABLES] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_NTVARIABLES_INCUPSIPS] FOREIGN KEY ([CODSERIPS]) REFERENCES [dbo].[INCUPSIPS] ([CODSERIPS]),
    CONSTRAINT [FK_NTVARIABLES_RSGRUPO] FOREIGN KEY ([IDNTGRUPO]) REFERENCES [dbo].[NTGRUPOS] ([ID])
);


GO
ALTER TABLE [dbo].[NTVARIABLES] NOCHECK CONSTRAINT [FK_NTVARIABLES_INCUPSIPS];


GO
ALTER TABLE [dbo].[NTVARIABLES] NOCHECK CONSTRAINT [FK_NTVARIABLES_RSGRUPO];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Campo que guarda la opción de solo lectura de las variables por revisión por sistema (true o false)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'NTVARIABLES', @level2type = N'COLUMN', @level2name = N'OCULTOVERDADERO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del servicio IPS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'NTVARIABLES', @level2type = N'COLUMN', @level2name = N'CODSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Genero a la que aplica la variable  1  - Masculino  2 - Femenino  3  - los dos generos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'NTVARIABLES', @level2type = N'COLUMN', @level2name = N'GENERO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de variable  1 - BOOLEAN - CONTROL CHECK  2 - STRING - CONTROL MEMOEDIT  3 - NUMERICO - NUMERIC  4- LISTA - CONTROL GRIDLOOKUPEDIT  5- FECHA DATE TIME', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'NTVARIABLES', @level2type = N'COLUMN', @level2name = N'TIPO';

