CREATE TABLE [dbo].[HCNUTPAREND] (
    [ID]           INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDHCNUTPAREC] INT             NOT NULL,
    [IDHCPARNUTC]  INT             NOT NULL,
    [IDHCPARNUTD]  INT             NOT NULL,
    [NAMENUT]      CHAR (300)      NULL,
    [CODPRODUC]    CHAR (20)       NOT NULL,
    [CODUNIMED]    VARCHAR (20)    NOT NULL,
    [APORTENUT]    NUMERIC (18, 3) NULL,
    [VOLUMENCAL]   NUMERIC (18, 3) NOT NULL,
    [FORVOLUADM]   VARCHAR (2000)  NOT NULL,
    [RVOLUADM]     VARCHAR (1000)  NOT NULL,
    [CORRPURGACAL] NUMERIC (18, 3) NOT NULL,
    [FORCORRPURGA] VARCHAR (2000)  NOT NULL,
    [RCORRPURGA]   VARCHAR (1000)  NULL,
    [CANTMED]      INT             NULL,
    CONSTRAINT [PK__HCNUTPAR__3214EC27204B518A] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCNUTPAREC_HCNUTPAREND] FOREIGN KEY ([IDHCNUTPAREC]) REFERENCES [dbo].[HCNUTPAREC] ([ID]),
    CONSTRAINT [FK_HCPARNUTC_HCNUTPAREND] FOREIGN KEY ([IDHCPARNUTC]) REFERENCES [dbo].[HCPARNUTC] ([ID]),
    CONSTRAINT [FK_HCPARNUTD_HCNUTPAREND] FOREIGN KEY ([IDHCPARNUTD]) REFERENCES [dbo].[HCPARNUTD] ([ID]),
    CONSTRAINT [FK_IHLISTPRO_HCNUTPAREND] FOREIGN KEY ([CODPRODUC]) REFERENCES [dbo].[IHLISTPRO] ([CODPRODUC])
);


GO
ALTER TABLE [dbo].[HCNUTPAREND] NOCHECK CONSTRAINT [FK_HCNUTPAREC_HCNUTPAREND];


GO
ALTER TABLE [dbo].[HCNUTPAREND] NOCHECK CONSTRAINT [FK_HCPARNUTC_HCNUTPAREND];


GO
ALTER TABLE [dbo].[HCNUTPAREND] NOCHECK CONSTRAINT [FK_HCPARNUTD_HCNUTPAREND];


GO
ALTER TABLE [dbo].[HCNUTPAREND] NOCHECK CONSTRAINT [FK_IHLISTPRO_HCNUTPAREND];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cantidad de medicamento solicitada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREND', @level2type = N'COLUMN', @level2name = N'CANTMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Formula de la correccion de purga (Reemplazada)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREND', @level2type = N'COLUMN', @level2name = N'RCORRPURGA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Formula de la correcion de purga', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREND', @level2type = N'COLUMN', @level2name = N'FORCORRPURGA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor de la correccion de purga', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREND', @level2type = N'COLUMN', @level2name = N'CORRPURGACAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Formula de volumen administrar (reemplazada con los valores numericos)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREND', @level2type = N'COLUMN', @level2name = N'RVOLUADM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Formula del volumen administrar del nutriente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREND', @level2type = N'COLUMN', @level2name = N'FORVOLUADM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Volumen administrar al paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREND', @level2type = N'COLUMN', @level2name = N'VOLUMENCAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Aporte subministrado del nutriente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREND', @level2type = N'COLUMN', @level2name = N'APORTENUT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la unidad del medicamento asociado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREND', @level2type = N'COLUMN', @level2name = N'CODUNIMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Medicamento asociado al nutriente parenteral', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREND', @level2type = N'COLUMN', @level2name = N'CODPRODUC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre del nutriente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREND', @level2type = N'COLUMN', @level2name = N'NAMENUT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID de la pametrizacion de los nutrientes parenterales', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREND', @level2type = N'COLUMN', @level2name = N'IDHCPARNUTD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID de la pametrizacion de nutricion parenteral', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREND', @level2type = N'COLUMN', @level2name = N'IDHCPARNUTC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID de la cabecera de la prescripción', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREND', @level2type = N'COLUMN', @level2name = N'IDHCNUTPAREC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREND', @level2type = N'COLUMN', @level2name = N'ID';

