CREATE TABLE [dbo].[HCREPORTESP] (
    [ID]            INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDPRMODELOHC]  INT           NOT NULL,
    [NOMBREVISTA]   VARCHAR (150) NOT NULL,
    [NOMBREREPORTE] VARCHAR (150) NOT NULL,
    [FECHACREA]     DATETIME      NOT NULL,
    [USUCREA]       CHAR (20)     NOT NULL,
    [FECHAMOD]      DATETIME      NULL,
    [USUMOD]        CHAR (20)     NULL,
    [TIPO]          INT           NULL,
    CONSTRAINT [PK_HCREPORTESP] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCREPORTESP_PRMODELOHC] FOREIGN KEY ([IDPRMODELOHC]) REFERENCES [dbo].[PRMODELOHC] ([ID])
);


GO
ALTER TABLE [dbo].[HCREPORTESP] NOCHECK CONSTRAINT [FK_HCREPORTESP_PRMODELOHC];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1 - ''modelo HC parametrizables  2 - ''Notas Administrativas  3 - ''Otros Reportes  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREPORTESP', @level2type = N'COLUMN', @level2name = N'TIPO';

