CREATE TABLE [dbo].[CALTIPOCLASE] (
    [ID]            INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [TIPOACCION]    INT           NOT NULL,
    [CODIGO]        VARCHAR (5)   NOT NULL,
    [DESCRIPCION]   VARCHAR (400) NULL,
    [IDPADRE]       INT           NULL,
    [CONTROLVIGILA] INT           NULL,
    CONSTRAINT [PK_CALTIPOCLASE] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_CALTIPOCLASE_CALTIPOCLASE] FOREIGN KEY ([IDPADRE]) REFERENCES [dbo].[CALTIPOCLASE] ([ID])
);


GO
ALTER TABLE [dbo].[CALTIPOCLASE] NOCHECK CONSTRAINT [FK_CALTIPOCLASE_CALTIPOCLASE];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Control y vigilancia:  1=Tecnovigilancia  2=Farmacovigilancia  3=Hemovigilancia  4=Reactivovigilancia  5=IAAS-Dispositivos  6=Infección de localización quirúrgica  7=Ninguno', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTIPOCLASE', @level2type = N'COLUMN', @level2name = N'CONTROLVIGILA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '  Guardar o actualizar datos de registros Tipografico  1 = Accion Insegura    Guarda o actualiza datos de registros morfologicos    2 = Accion clinica Insesperada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTIPOCLASE', @level2type = N'COLUMN', @level2name = N'TIPOACCION';

