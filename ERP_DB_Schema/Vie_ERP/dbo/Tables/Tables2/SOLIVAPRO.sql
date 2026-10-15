CREATE TABLE [dbo].[SOLIVAPRO] (
    [SOAUTOIVA] TINYINT        IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [SODESCIVA] VARCHAR (50)   NOT NULL,
    [SOPORCIVA] NUMERIC (5, 2) NOT NULL,
    [SOCODIVA]  VARCHAR (20)   NOT NULL,
    CONSTRAINT [PK_SOLIVAPRO] PRIMARY KEY CLUSTERED ([SOAUTOIVA] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Contiene el codigo del iva', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLIVAPRO', @level2type = N'COLUMN', @level2name = N'SOCODIVA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor del iva', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLIVAPRO', @level2type = N'COLUMN', @level2name = N'SOPORCIVA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Desripcion del iva', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLIVAPRO', @level2type = N'COLUMN', @level2name = N'SODESCIVA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'autonumerico del iva', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLIVAPRO', @level2type = N'COLUMN', @level2name = N'SOAUTOIVA';

