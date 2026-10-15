CREATE TABLE [dbo].[HCDIAGRAMAS] (
    [ID]              INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [TIPODIAGRAMA]    INT                                                                              NOT NULL,
    [DIAGRAMA]        VARBINARY (MAX)                                                                  NOT NULL,
    [CODCENATE]       CHAR (10)                                                                        NOT NULL,
    [UFUCODIGO]       CHAR (10)                                                                        NOT NULL,
    [IPCODPACI]       VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES]       CHAR (10)                                                                        NOT NULL,
    [NUMEFOLIO]       CHAR (10)                                                                        NULL,
    [USUARIOCREACION] CHAR (20)                                                                        NOT NULL,
    [FECHACREACION]   DATETIME                                                                         NOT NULL,
    [USUARIOMODIFICO] CHAR (20)                                                                        NULL,
    [FECHAMODIFICO]   DATETIME                                                                         NULL,
    [DiagramImage]    VARBINARY (MAX)                                                                  NULL,
    CONSTRAINT [PK_HCFAMILIECOMAPA] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCFAMILIECOMAPA_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCFAMILIECOMAPA_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCFAMILIECOMAPA_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_HCFAMILIECOMAPA_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO])
);


GO
ALTER TABLE [dbo].[HCDIAGRAMAS] NOCHECK CONSTRAINT [FK_HCFAMILIECOMAPA_ADCENATEN];


GO
ALTER TABLE [dbo].[HCDIAGRAMAS] NOCHECK CONSTRAINT [FK_HCFAMILIECOMAPA_ADINGRESO];


GO
ALTER TABLE [dbo].[HCDIAGRAMAS] NOCHECK CONSTRAINT [FK_HCFAMILIECOMAPA_INPACIENT];


GO
ALTER TABLE [dbo].[HCDIAGRAMAS] NOCHECK CONSTRAINT [FK_HCFAMILIECOMAPA_INUNIFUNC];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la imagen de diagrama ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDIAGRAMAS', @level2type = N'COLUMN', @level2name = N'DiagramImage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Modificacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDIAGRAMAS', @level2type = N'COLUMN', @level2name = N'FECHAMODIFICO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'usuario que modifico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDIAGRAMAS', @level2type = N'COLUMN', @level2name = N'USUARIOMODIFICO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha creación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDIAGRAMAS', @level2type = N'COLUMN', @level2name = N'FECHACREACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario creacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDIAGRAMAS', @level2type = N'COLUMN', @level2name = N'USUARIOCREACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de folio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDIAGRAMAS', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de Ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDIAGRAMAS', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cedula paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDIAGRAMAS', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDIAGRAMAS', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Centro Atención', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDIAGRAMAS', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Guarda Resultado o diagramo en bits creado en Familiograma ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDIAGRAMAS', @level2type = N'COLUMN', @level2name = N'DIAGRAMA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo Diagrama  1 - Ecomapa  2 - Familograma  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDIAGRAMAS', @level2type = N'COLUMN', @level2name = N'TIPODIAGRAMA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDIAGRAMAS', @level2type = N'COLUMN', @level2name = N'ID';

