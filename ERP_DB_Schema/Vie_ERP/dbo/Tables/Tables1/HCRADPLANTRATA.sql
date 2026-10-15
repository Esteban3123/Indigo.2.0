CREATE TABLE [dbo].[HCRADPLANTRATA] (
    [ID]             INT            IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDHCRADINFOC]   INT            NOT NULL,
    [NUMINGRES]      CHAR (10)      NOT NULL,
    [FECHORDEN]      DATETIME       NOT NULL,
    [NUMREGION]      VARCHAR (3)    NOT NULL,
    [NOMREGION]      VARCHAR (100)  NOT NULL,
    [DOSTOTAL]       INT            NOT NULL,
    [DOSISPORFRACC]  INT            NOT NULL,
    [NUMEFRACC]      INT            NOT NULL,
    [FECCALCULO]     DATETIME       NOT NULL,
    [PORCDOSTOTAUTO] INT            NULL,
    [OBSERVACION]    VARCHAR (2000) NULL,
    [ESTADO]         INT            NULL,
    [FIRMFISICO]     CHAR (20)      NULL,
    [FIRMRADION]     CHAR (20)      NULL,
    [FECFINALIZA]    DATETIME       NULL,
    [DESFINALIZA]    VARCHAR (MAX)  NULL,
    [IDHCRADORD]     INT            NULL,
    [ESTADOCITA]     INT            NULL,
    CONSTRAINT [PK_HCRADPLANTRATA] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCRADPLANTRATA_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCRADPLANTRATA_HCRADINFOC] FOREIGN KEY ([IDHCRADINFOC]) REFERENCES [dbo].[HCRADINFOC] ([ID]),
    CONSTRAINT [FK_HCRADPLANTRATA_HCRADPLANTRATA] FOREIGN KEY ([ID]) REFERENCES [dbo].[HCRADPLANTRATA] ([ID]),
    CONSTRAINT [FK_HCRADPLANTRATA_INPROFSAL] FOREIGN KEY ([FIRMFISICO]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_HCRADPLANTRATA_INPROFSAL1] FOREIGN KEY ([FIRMRADION]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL])
);


GO
ALTER TABLE [dbo].[HCRADPLANTRATA] NOCHECK CONSTRAINT [FK_HCRADPLANTRATA_ADINGRESO];


GO
ALTER TABLE [dbo].[HCRADPLANTRATA] NOCHECK CONSTRAINT [FK_HCRADPLANTRATA_HCRADINFOC];


GO
ALTER TABLE [dbo].[HCRADPLANTRATA] NOCHECK CONSTRAINT [FK_HCRADPLANTRATA_HCRADPLANTRATA];


GO
ALTER TABLE [dbo].[HCRADPLANTRATA] NOCHECK CONSTRAINT [FK_HCRADPLANTRATA_INPROFSAL];


GO
ALTER TABLE [dbo].[HCRADPLANTRATA] NOCHECK CONSTRAINT [FK_HCRADPLANTRATA_INPROFSAL1];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de la cita', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADPLANTRATA', @level2type = N'COLUMN', @level2name = N'ESTADOCITA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda el id de la orden', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADPLANTRATA', @level2type = N'COLUMN', @level2name = N'IDHCRADORD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda la descripción de la finalización', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADPLANTRATA', @level2type = N'COLUMN', @level2name = N'DESFINALIZA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de finalización', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADPLANTRATA', @level2type = N'COLUMN', @level2name = N'FECFINALIZA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda la firma nueva del radiologo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADPLANTRATA', @level2type = N'COLUMN', @level2name = N'FIRMRADION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Firma fisica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADPLANTRATA', @level2type = N'COLUMN', @level2name = N'FIRMFISICO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADPLANTRATA', @level2type = N'COLUMN', @level2name = N'ESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Observación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADPLANTRATA', @level2type = N'COLUMN', @level2name = N'OBSERVACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Iso dosis autorizado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADPLANTRATA', @level2type = N'COLUMN', @level2name = N'PORCDOSTOTAUTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de calculo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADPLANTRATA', @level2type = N'COLUMN', @level2name = N'FECCALCULO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de fracciones ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADPLANTRATA', @level2type = N'COLUMN', @level2name = N'NUMEFRACC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Dosis por fracción ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADPLANTRATA', @level2type = N'COLUMN', @level2name = N'DOSISPORFRACC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Dosis total', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADPLANTRATA', @level2type = N'COLUMN', @level2name = N'DOSTOTAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre de la region ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADPLANTRATA', @level2type = N'COLUMN', @level2name = N'NOMREGION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de la region ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADPLANTRATA', @level2type = N'COLUMN', @level2name = N'NUMREGION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de la orden ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADPLANTRATA', @level2type = N'COLUMN', @level2name = N'FECHORDEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero del Ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADPLANTRATA', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Relacion con la cabecera', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADPLANTRATA', @level2type = N'COLUMN', @level2name = N'IDHCRADINFOC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADPLANTRATA', @level2type = N'COLUMN', @level2name = N'ID';

