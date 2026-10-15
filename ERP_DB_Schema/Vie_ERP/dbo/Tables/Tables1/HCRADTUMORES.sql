CREATE TABLE [dbo].[HCRADTUMORES] (
    [ID]              INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDHCRADESQUEMAS] INT           NOT NULL,
    [NOMBRETUMOR]     VARCHAR (150) NOT NULL,
    [FECHACREA]       DATETIME      NOT NULL,
    [USUCREA]         CHAR (20)     NOT NULL,
    CONSTRAINT [PK_HCRADTUMORES] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCRADTUMORES_HCRADESQUEMAS] FOREIGN KEY ([IDHCRADESQUEMAS]) REFERENCES [dbo].[HCRADESQUEMAS] ([ID])
);


GO
ALTER TABLE [dbo].[HCRADTUMORES] NOCHECK CONSTRAINT [FK_HCRADTUMORES_HCRADESQUEMAS];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'usuario de creacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADTUMORES', @level2type = N'COLUMN', @level2name = N'USUCREA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de creacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADTUMORES', @level2type = N'COLUMN', @level2name = N'FECHACREA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre del tumor', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADTUMORES', @level2type = N'COLUMN', @level2name = N'NOMBRETUMOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID de relacion con el esquema de radioterapia (HCRADESQUEMA)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADTUMORES', @level2type = N'COLUMN', @level2name = N'IDHCRADESQUEMAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico de tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADTUMORES', @level2type = N'COLUMN', @level2name = N'ID';

