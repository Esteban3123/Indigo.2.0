CREATE TABLE [dbo].[HCPARLABO] (
    [CODCENATE]           CHAR (10)     NOT NULL,
    [INTLABOAC]           BIT           NOT NULL,
    [URISERVIC]           VARCHAR (250) NULL,
    [PASSINTER]           VARCHAR (150) NULL,
    [EXAENUF]             BIT           CONSTRAINT [DF_HCPARLABO_EXAENUF] DEFAULT ((0)) NOT NULL,
    [TIPOPROVEEDOR]       INT           NULL,
    [USUARIOAUTHBASICA]   VARCHAR (150) NULL,
    [MENSAJERECIBIDO]     VARCHAR (250) NULL,
    [USUARIOCREACION]     CHAR (20)     NULL,
    [FECHACREACION]       DATETIME      NULL,
    [USUARIOMODIFICACION] CHAR (20)     NULL,
    [FECHAMODIFICACION]   DATETIME      NULL,
    CONSTRAINT [PK_HCPARLABO] PRIMARY KEY CLUSTERED ([CODCENATE] ASC),
    CONSTRAINT [FK_HCPARLABO_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCPARLABO_SEGusuaru_1] FOREIGN KEY ([USUARIOCREACION]) REFERENCES [dbo].[SEGusuaru] ([CODUSUARI]),
    CONSTRAINT [FK_HCPARLABO_SEGusuaru_2] FOREIGN KEY ([USUARIOMODIFICACION]) REFERENCES [dbo].[SEGusuaru] ([CODUSUARI])
);


GO
ALTER TABLE [dbo].[HCPARLABO] NOCHECK CONSTRAINT [FK_HCPARLABO_ADCENATEN];


GO
ALTER TABLE [dbo].[HCPARLABO] NOCHECK CONSTRAINT [FK_HCPARLABO_SEGusuaru_1];


GO
ALTER TABLE [dbo].[HCPARLABO] NOCHECK CONSTRAINT [FK_HCPARLABO_SEGusuaru_2];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Mensaje de confirmacion de recepcion exitosa por parte de la interfaz    EJ:  Solicitud procesada  Trama XML registrada  MENSAJE RECIBIDO  Ingreso de órden', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARLABO', @level2type = N'COLUMN', @level2name = N'MENSAJERECIBIDO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Ninguno = 0  Enterprise = 1  Athenea = 2  Annarlab = 3  COmprolab = 4  Nobilis = 5  Medife = 6', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARLABO', @level2type = N'COLUMN', @level2name = N'TIPOPROVEEDOR';

