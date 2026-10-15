CREATE TABLE [dbo].[UREPPERMISOR] (
    [ID]         INT      IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ID_REPORTE] INT      NOT NULL,
    [codigorol]  CHAR (4) NOT NULL,
    CONSTRAINT [PK_UREPPERMISOR] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_UREPPERMISOR_SEGrolesu] FOREIGN KEY ([codigorol]) REFERENCES [dbo].[SEGrolesu] ([codigorol]),
    CONSTRAINT [FK_UREPPERMISOR_UREPPERSON] FOREIGN KEY ([ID_REPORTE]) REFERENCES [dbo].[UREPPERSON] ([ID])
);


GO
ALTER TABLE [dbo].[UREPPERMISOR] NOCHECK CONSTRAINT [FK_UREPPERMISOR_SEGrolesu];


GO
ALTER TABLE [dbo].[UREPPERMISOR] NOCHECK CONSTRAINT [FK_UREPPERMISOR_UREPPERSON];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del rol', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'UREPPERMISOR', @level2type = N'COLUMN', @level2name = N'codigorol';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID del reporte', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'UREPPERMISOR', @level2type = N'COLUMN', @level2name = N'ID_REPORTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'UREPPERMISOR', @level2type = N'COLUMN', @level2name = N'ID';

