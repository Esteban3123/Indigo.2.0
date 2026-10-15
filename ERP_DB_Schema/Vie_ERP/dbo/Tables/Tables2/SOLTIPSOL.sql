CREATE TABLE [dbo].[SOLTIPSOL] (
    [AUTO]     INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODTISOL] VARCHAR (3)  NOT NULL,
    [DESTISOL] VARCHAR (60) NOT NULL,
    CONSTRAINT [PK_SOLTIPSOL] PRIMARY KEY CLUSTERED ([AUTO] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Contiene el nombre del tipo de solicitud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLTIPSOL', @level2type = N'COLUMN', @level2name = N'DESTISOL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'CONTIENE EL CODIGO DEL TIPO DE SOLICITU', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLTIPSOL', @level2type = N'COLUMN', @level2name = N'CODTISOL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Contiene el autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLTIPSOL', @level2type = N'COLUMN', @level2name = N'AUTO';

