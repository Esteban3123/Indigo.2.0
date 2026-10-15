CREATE TABLE [dbo].[HCEGRESINRESULT] (
    [CODSERIPS]                CHAR (20) NOT NULL,
    [UFUCODIGO]                CHAR (10) NOT NULL,
    [CODCENATE]                CHAR (10) NOT NULL,
    [IDDESCRIPCIONRELACIONADA] INT       NULL,
    [ID]                       INT       IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [USUARIOCREACION]          CHAR (20) NULL,
    [FECHACREACION]            DATETIME  NULL,
    [USUARIOMODIFICACION]      CHAR (20) NULL,
    [FECHAMODIFICACION]        DATETIME  NULL,
    CONSTRAINT [PK_HCEGRESINRESULT] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_EGRESINRESULT_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_EGRESINRESULT_INCUPSIPS] FOREIGN KEY ([CODSERIPS]) REFERENCES [dbo].[INCUPSIPS] ([CODSERIPS]),
    CONSTRAINT [FK_EGRESINRESULT_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO]),
    CONSTRAINT [FK_HCEGRESINRESULT_SEGusuaru_1] FOREIGN KEY ([USUARIOCREACION]) REFERENCES [dbo].[SEGusuaru] ([CODUSUARI]),
    CONSTRAINT [FK_HCEGRESINRESULT_SEGusuaru_2] FOREIGN KEY ([USUARIOMODIFICACION]) REFERENCES [dbo].[SEGusuaru] ([CODUSUARI])
);


GO
ALTER TABLE [dbo].[HCEGRESINRESULT] NOCHECK CONSTRAINT [FK_EGRESINRESULT_ADCENATEN];


GO
ALTER TABLE [dbo].[HCEGRESINRESULT] NOCHECK CONSTRAINT [FK_EGRESINRESULT_INCUPSIPS];


GO
ALTER TABLE [dbo].[HCEGRESINRESULT] NOCHECK CONSTRAINT [FK_EGRESINRESULT_INUNIFUNC];


GO
ALTER TABLE [dbo].[HCEGRESINRESULT] NOCHECK CONSTRAINT [FK_HCEGRESINRESULT_SEGusuaru_1];


GO
ALTER TABLE [dbo].[HCEGRESINRESULT] NOCHECK CONSTRAINT [FK_HCEGRESINRESULT_SEGusuaru_2];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Modificación del Registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEGRESINRESULT', @level2type = N'COLUMN', @level2name = N'FECHAMODIFICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Modificación del Registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEGRESINRESULT', @level2type = N'COLUMN', @level2name = N'USUARIOMODIFICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Creación del Registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEGRESINRESULT', @level2type = N'COLUMN', @level2name = N'FECHACREACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Creación del Registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEGRESINRESULT', @level2type = N'COLUMN', @level2name = N'USUARIOCREACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEGRESINRESULT', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = ' ID de descripcion de relacion. relacion con  VIE ERP  (contract.CUPSEntityContractDescriptions) ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEGRESINRESULT', @level2type = N'COLUMN', @level2name = N'IDDESCRIPCIONRELACIONADA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del centro de atención', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEGRESINRESULT', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código de la unidad funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEGRESINRESULT', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el codigo del servicio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEGRESINRESULT', @level2type = N'COLUMN', @level2name = N'CODSERIPS';

