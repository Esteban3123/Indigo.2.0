CREATE TABLE [dbo].[AGEPROGQXMATERIALES] (
    [ID]          INT       IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDAGEPROGQX] INT       NOT NULL,
    [CODPRODUC]   CHAR (20) NOT NULL,
    [QUANTITY]    INT       NULL,
    CONSTRAINT [PK_AGEPROGQXMATERIALES] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_AGEPROGQXMATERIALES_AGEPROGQX] FOREIGN KEY ([IDAGEPROGQX]) REFERENCES [dbo].[AGEPROGQX] ([CODAUTONU]),
    CONSTRAINT [FK_AGEPROGQXMATERIALES_IHLISTPRO] FOREIGN KEY ([CODPRODUC]) REFERENCES [dbo].[IHLISTPRO] ([CODPRODUC])
);


GO
ALTER TABLE [dbo].[AGEPROGQXMATERIALES] NOCHECK CONSTRAINT [FK_AGEPROGQXMATERIALES_AGEPROGQX];


GO
ALTER TABLE [dbo].[AGEPROGQXMATERIALES] NOCHECK CONSTRAINT [FK_AGEPROGQXMATERIALES_IHLISTPRO];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda cantidad de otros', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQXMATERIALES', @level2type = N'COLUMN', @level2name = N'QUANTITY';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de producto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQXMATERIALES', @level2type = N'COLUMN', @level2name = N'CODPRODUC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID cirugia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQXMATERIALES', @level2type = N'COLUMN', @level2name = N'IDAGEPROGQX';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROGQXMATERIALES', @level2type = N'COLUMN', @level2name = N'ID';

