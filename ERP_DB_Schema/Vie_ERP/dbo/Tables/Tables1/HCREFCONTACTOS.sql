CREATE TABLE [dbo].[HCREFCONTACTOS] (
    [ID]           INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDHCREFCONTD] INT           NOT NULL,
    [EMAIL]        VARCHAR (100) NOT NULL,
    CONSTRAINT [PK_HCREFCONTACTOS] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCREFCONTACTOS_HCREFCONTD] FOREIGN KEY ([IDHCREFCONTD]) REFERENCES [dbo].[HCREFCONTD] ([ID])
);


GO
ALTER TABLE [dbo].[HCREFCONTACTOS] NOCHECK CONSTRAINT [FK_HCREFCONTACTOS_HCREFCONTD];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Correo electronico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONTACTOS', @level2type = N'COLUMN', @level2name = N'EMAIL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la gestion de la referencia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONTACTOS', @level2type = N'COLUMN', @level2name = N'IDHCREFCONTD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONTACTOS', @level2type = N'COLUMN', @level2name = N'ID';

