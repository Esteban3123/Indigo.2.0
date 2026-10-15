CREATE TABLE [dbo].[ADPA3047C] (
    [CODCENATE]          CHAR (10)     NOT NULL,
    [TIPENTREP]          CHAR (1)      NOT NULL,
    [INNUMTELL]          CHAR (5)      NULL,
    [NUTELCENL]          CHAR (7)      NULL,
    [NUEXTTELL]          CHAR (6)      NULL,
    [ENTEMAILL]          CHAR (50)     NULL,
    [INNUMTELD]          CHAR (5)      NOT NULL,
    [NUTELCEND]          CHAR (7)      NOT NULL,
    [NUEXTTELD]          CHAR (6)      NOT NULL,
    [ENTEMAILD]          CHAR (50)     NOT NULL,
    [CARDOCTRA]          CHAR (255)    NOT NULL,
    [INDAUDFOR]          NUMERIC (18)  NOT NULL,
    [EMAILREFECONT]      CHAR (100)    NULL,
    [ConectionFileShare] VARCHAR (450) NULL,
    CONSTRAINT [PK_ADPA3047C] PRIMARY KEY CLUSTERED ([CODCENATE] ASC),
    CONSTRAINT [FK_ADPA3047C_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE])
);


GO
ALTER TABLE [dbo].[ADPA3047C] NOCHECK CONSTRAINT [FK_ADPA3047C_ADCENATEN];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Conection string del Azure File Share de los documentos adjuntos.
->esta se usa en los servicios Backend del EHR.
->surje de la necesidad de una app service azure conectar con un azure file share
->actualmente se usa para cargar las lecturas de imagenologia de la integracion ODO - Aquila', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPA3047C', @level2type = N'COLUMN', @level2name = N'ConectionFileShare';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'campo para configurar  el E-Mail de donde se va enviar la referencia.C', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPA3047C', @level2type = N'COLUMN', @level2name = N'EMAILREFECONT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de Auditoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPA3047C', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Recurso Compartido para el Almacenamiento de Documentos Adicionales', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPA3047C', @level2type = N'COLUMN', @level2name = N'CARDOCTRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'E-Mail DTD', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPA3047C', @level2type = N'COLUMN', @level2name = N'ENTEMAILD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de Extension DTD', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPA3047C', @level2type = N'COLUMN', @level2name = N'NUEXTTELD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero Telefonico Fax DTD', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPA3047C', @level2type = N'COLUMN', @level2name = N'NUTELCEND';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Indicativo del Numero Telefonico Fax DTD', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPA3047C', @level2type = N'COLUMN', @level2name = N'INNUMTELD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'E-Mail DTL', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPA3047C', @level2type = N'COLUMN', @level2name = N'ENTEMAILL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de Extension DTL', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPA3047C', @level2type = N'COLUMN', @level2name = N'NUEXTTELL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero Telefonico Fax DTL', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPA3047C', @level2type = N'COLUMN', @level2name = N'NUTELCENL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Indicativo del Numero Telefonico Fax DTL', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPA3047C', @level2type = N'COLUMN', @level2name = N'INNUMTELL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el Tipo de Direccion Territorial de Salud a la que se reporta  1: DTL y DTD  2: DTD', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPA3047C', @level2type = N'COLUMN', @level2name = N'TIPENTREP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el Centron de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPA3047C', @level2type = N'COLUMN', @level2name = N'CODCENATE';

