CREATE TABLE [dbo].[HCSOPORTECACPACIENTED] (
    [ID]                     INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDHCSOPORTECAC]         INT           NOT NULL,
    [IDHCSOPORTECACPACIENTE] INT           NOT NULL,
    [IPCODPACI]              VARCHAR (25)  NOT NULL,
    [MENSAJE]                VARCHAR (MAX) NOT NULL,
    [FECHACREACION]          DATETIME      NOT NULL,
    CONSTRAINT [PK_HCSOPORTECACPACIENTED] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCSOPORTECACPACIENTED_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_HCSOPORTECACPACIENTED_SoporteCAC] FOREIGN KEY ([IDHCSOPORTECAC]) REFERENCES [dbo].[HCSOPORTECAC] ([ID]),
    CONSTRAINT [FK_HCSOPORTECACPACIENTED_SoporteCACD] FOREIGN KEY ([IDHCSOPORTECACPACIENTE]) REFERENCES [dbo].[HCSOPORTECACPACIENTE] ([ID])
);


GO
ALTER TABLE [dbo].[HCSOPORTECACPACIENTED] NOCHECK CONSTRAINT [FK_HCSOPORTECACPACIENTED_INPACIENT];


GO
ALTER TABLE [dbo].[HCSOPORTECACPACIENTED] NOCHECK CONSTRAINT [FK_HCSOPORTECACPACIENTED_SoporteCAC];


GO
ALTER TABLE [dbo].[HCSOPORTECACPACIENTED] NOCHECK CONSTRAINT [FK_HCSOPORTECACPACIENTED_SoporteCACD];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'fecha Creacion del registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOPORTECACPACIENTED', @level2type = N'COLUMN', @level2name = N'FECHACREACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Mensaje capturado por el cacth ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOPORTECACPACIENTED', @level2type = N'COLUMN', @level2name = N'MENSAJE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificacion del paciente que presenta error al generar soporte CAC', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOPORTECACPACIENTED', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de relacion con la tabla de paciente de soporte CAC (HCSOPORTECACPACIENT)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOPORTECACPACIENTED', @level2type = N'COLUMN', @level2name = N'IDHCSOPORTECACPACIENTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id cabecera de soporte CAC (HCSOPORTECAC)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOPORTECACPACIENTED', @level2type = N'COLUMN', @level2name = N'IDHCSOPORTECAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOPORTECACPACIENTED', @level2type = N'COLUMN', @level2name = N'ID';

