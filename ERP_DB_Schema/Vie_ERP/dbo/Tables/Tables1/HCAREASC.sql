CREATE TABLE [dbo].[HCAREASC] (
    [ID]        INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODCENATE] CHAR (10)     NOT NULL,
    [CODAREA]   VARCHAR (4)   NOT NULL,
    [DESAREA]   VARCHAR (150) NOT NULL,
    [ESTADO]    BIT           NOT NULL,
    [FECREGSIS] DATETIME      NOT NULL,
    [REGUSUARI] CHAR (20)     NOT NULL,
    [FECMODRE]  DATETIME      NULL,
    [REGUSUMOD] CHAR (20)     NULL,
    [UFUCODIGO] CHAR (10)     NULL,
    CONSTRAINT [PK_HCAREASC] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCAREASC_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCAREASC_HCAREASC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO])
);


GO
ALTER TABLE [dbo].[HCAREASC] NOCHECK CONSTRAINT [FK_HCAREASC_ADCENATEN];


GO
ALTER TABLE [dbo].[HCAREASC] NOCHECK CONSTRAINT [FK_HCAREASC_HCAREASC];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Unidad funcional a la que pertenece el area de servicio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAREASC', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario de Modificacion ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAREASC', @level2type = N'COLUMN', @level2name = N'REGUSUMOD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Modificacion Registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAREASC', @level2type = N'COLUMN', @level2name = N'FECMODRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario creacion Registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAREASC', @level2type = N'COLUMN', @level2name = N'REGUSUARI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha creacion Registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAREASC', @level2type = N'COLUMN', @level2name = N'FECREGSIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado 1 - Activo - 0 - Inactivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAREASC', @level2type = N'COLUMN', @level2name = N'ESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripcion Area', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAREASC', @level2type = N'COLUMN', @level2name = N'DESAREA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo Area', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAREASC', @level2type = N'COLUMN', @level2name = N'CODAREA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Centro Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAREASC', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico de la Tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAREASC', @level2type = N'COLUMN', @level2name = N'ID';

