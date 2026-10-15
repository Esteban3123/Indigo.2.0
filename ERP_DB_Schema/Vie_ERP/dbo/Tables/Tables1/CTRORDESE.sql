CREATE TABLE [dbo].[CTRORDESE] (
    [AUTO]      INT       IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [NUMINGRES] CHAR (10) NOT NULL,
    [SOSORDSER] CHAR (10) NOT NULL,
    [ORIGEN]    CHAR (1)  NOT NULL,
    CONSTRAINT [PK_CTRORDESE] PRIMARY KEY CLUSTERED ([AUTO] ASC),
    CONSTRAINT [FK_CTRORDESE_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES])
);


GO
ALTER TABLE [dbo].[CTRORDESE] NOCHECK CONSTRAINT [FK_CTRORDESE_ADINGRESO];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Origen desde donde fue creada la orden de servicio 1-Control Consulta Externa 2-  Liquidador 3-Control Servicio Ambulatorios', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CTRORDESE', @level2type = N'COLUMN', @level2name = N'ORIGEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de la orden asociada al ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CTRORDESE', @level2type = N'COLUMN', @level2name = N'SOSORDSER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero del ingreso en que se genero la orden', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CTRORDESE', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico de la orden', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CTRORDESE', @level2type = N'COLUMN', @level2name = N'AUTO';

