CREATE TABLE [dbo].[HCAUDITORIA] (
    [ID]            INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODUSUCONS]    CHAR (15)     NULL,
    [CODPACQCON]    VARCHAR (25)  NULL,
    [FECHCONSU]     DATETIME      NULL,
    [FECHFINCONS]   DATETIME      NULL,
    [NOMMAQCONS]    VARCHAR (50)  NULL,
    [IPMAQCONS]     VARCHAR (50)  NULL,
    [MOVCONSULHCID] INT           NULL,
    [OBSERVACION]   VARCHAR (300) NULL,
    [INGRESO]       VARCHAR (15)  NULL,
    [FOLIOIN]       VARCHAR (10)  NULL,
    [ANONIMATO]     BIT           NULL,
    [FOLIOFINAL]    VARCHAR (10)  NULL,
    [UFUCODIGO]     CHAR (10)     NULL,
    CONSTRAINT [PK_HCAUDITORIA] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCAUDITORIA_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO]),
    CONSTRAINT [FK_HCAUDITORIA_MOVCONSULHC] FOREIGN KEY ([MOVCONSULHCID]) REFERENCES [dbo].[MOVCONSULHC] ([Id])
);




GO



GO





GO



GO



GO
CREATE NONCLUSTERED INDEX [Consulta_Historia]
    ON [dbo].[HCAUDITORIA]([CODUSUCONS] ASC, [FECHCONSU] ASC)
    INCLUDE([CODPACQCON], [FECHFINCONS], [NOMMAQCONS], [IPMAQCONS], [MOVCONSULHCID], [OBSERVACION], [INGRESO]);


GO
ALTER INDEX [Consulta_Historia]
    ON [dbo].[HCAUDITORIA] DISABLE;




GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la Unidad Funcional (FK INUNIFUNC). Identificador de la unidad asistencial, centro de atención o servicio donde se realizó la consulta a historia clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAUDITORIA', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAUDITORIA', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAUDITORIA', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Folio final del paciente en la consulta actual. Número de página o secuencia final del registro de atención consultado en historia clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAUDITORIA', @level2type = N'COLUMN', @level2name = N'FOLIOFINAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo del folio final del paciente de la consulta actual', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAUDITORIA', @level2type = N'COLUMN', @level2name = N'FOLIOFINAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAUDITORIA', @level2type = N'COLUMN', @level2name = N'FOLIOFINAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de anonimato del paciente (BIT, 0/1). Flag que marca si el paciente está bajo protección de identidad o consulta anónima en la historia clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAUDITORIA', @level2type = N'COLUMN', @level2name = N'ANONIMATO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo que indica si el paciente esta bajo anonimato', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAUDITORIA', @level2type = N'COLUMN', @level2name = N'ANONIMATO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAUDITORIA', @level2type = N'COLUMN', @level2name = N'ANONIMATO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Folio inicial del paciente en la consulta actual. Número de página o secuencia inicial del registro de atención consultado en historia clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAUDITORIA', @level2type = N'COLUMN', @level2name = N'FOLIOIN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo del folio inicial del paciente de la consulta actual', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAUDITORIA', @level2type = N'COLUMN', @level2name = N'FOLIOIN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAUDITORIA', @level2type = N'COLUMN', @level2name = N'FOLIOIN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de ingreso del paciente en la consulta actual. Identificador único del episodio de hospitalización o atención ambulatoria.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAUDITORIA', @level2type = N'COLUMN', @level2name = N'INGRESO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo de ingreso del paciente de la consulta actual', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAUDITORIA', @level2type = N'COLUMN', @level2name = N'INGRESO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAUDITORIA', @level2type = N'COLUMN', @level2name = N'INGRESO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observación de la consulta a historia clínica. Notas, comentarios o detalles adicionales sobre la consulta realizada (VARCHAR 300).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAUDITORIA', @level2type = N'COLUMN', @level2name = N'OBSERVACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observacion de la consulta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAUDITORIA', @level2type = N'COLUMN', @level2name = N'OBSERVACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAUDITORIA', @level2type = N'COLUMN', @level2name = N'OBSERVACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Motivo de la consulta (FK MOVCONSULHC). Identificador numérico del tipo de acceso, movimiento o razón de la consulta a la historia clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAUDITORIA', @level2type = N'COLUMN', @level2name = N'MOVCONSULHCID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Motivo de la consulta ID de la tabla MOVCONSULHC', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAUDITORIA', @level2type = N'COLUMN', @level2name = N'MOVCONSULHCID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAUDITORIA', @level2type = N'COLUMN', @level2name = N'MOVCONSULHCID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dirección IP de la máquina desde donde se consultó la historia. Identificación técnica de la estación de trabajo o dispositivo accedió a la historia clínica (PII, auditoría de acceso).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAUDITORIA', @level2type = N'COLUMN', @level2name = N'IPMAQCONS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Direccion Ip  de la maquina de donde se consulta la historia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAUDITORIA', @level2type = N'COLUMN', @level2name = N'IPMAQCONS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAUDITORIA', @level2type = N'COLUMN', @level2name = N'IPMAQCONS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre de la máquina desde donde se consultó la historia. Identificador del computador, dispositivo o terminal que accedió a la historia clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAUDITORIA', @level2type = N'COLUMN', @level2name = N'NOMMAQCONS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre de la maquina de donde se consulta la historia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAUDITORIA', @level2type = N'COLUMN', @level2name = N'NOMMAQCONS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAUDITORIA', @level2type = N'COLUMN', @level2name = N'NOMMAQCONS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de finalización de la consulta a historia clínica. Timestamp DATETIME del fin del acceso.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAUDITORIA', @level2type = N'COLUMN', @level2name = N'FECHFINCONS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha finaliza la consulta de la historia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAUDITORIA', @level2type = N'COLUMN', @level2name = N'FECHFINCONS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAUDITORIA', @level2type = N'COLUMN', @level2name = N'FECHFINCONS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de la consulta a historia clínica. Timestamp DATETIME de inicio del acceso al registro.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAUDITORIA', @level2type = N'COLUMN', @level2name = N'FECHCONSU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de la consulta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAUDITORIA', @level2type = N'COLUMN', @level2name = N'FECHCONSU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAUDITORIA', @level2type = N'COLUMN', @level2name = N'FECHCONSU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del paciente consultado. Identificador único del paciente (cédula, documento, número de afiliado) cuya historia clínica fue accedida.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAUDITORIA', @level2type = N'COLUMN', @level2name = N'CODPACQCON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del pacinte al que consultan ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAUDITORIA', @level2type = N'COLUMN', @level2name = N'CODPACQCON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAUDITORIA', @level2type = N'COLUMN', @level2name = N'CODPACQCON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del usuario que consultó la historia clínica. Identificador del profesional de salud, administrativo o personal que realizó el acceso.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAUDITORIA', @level2type = N'COLUMN', @level2name = N'CODUSUCONS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo usuario que consulta la historia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAUDITORIA', @level2type = N'COLUMN', @level2name = N'CODUSUCONS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAUDITORIA', @level2type = N'COLUMN', @level2name = N'CODUSUCONS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único de la auditoría (PK, INT IDENTITY). Consecutivo de la transacción de consulta a historia clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAUDITORIA', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo Tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAUDITORIA', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAUDITORIA', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de auditoría de consultas a historias clínicas: guarda quién accedió a la historia clínica de un paciente, cuándo lo hizo, desde qué equipo y con qué propósito, permitiendo trazabilidad y control de acceso a información clínica sensible.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAUDITORIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAUDITORIA';
