CREATE TABLE [dbo].[ALERPACIE] (
    [AUTOALERT] INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IPCODPACI] VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [COMENALER] VARCHAR (500)                                                                    NULL,
    [FECHAREGI] DATETIME                                                                         NOT NULL,
    [CODUSUREG] CHAR (20)                                                                        NOT NULL,
    [FECHAINAC] DATETIME                                                                         NULL,
    [CODUSUINA] CHAR (20)                                                                        NULL,
    [ESTAALERT] CHAR (1)                                                                         NOT NULL,
    CONSTRAINT [PK_ALERPACIE] PRIMARY KEY CLUSTERED ([AUTOALERT] ASC),
    CONSTRAINT [FK_ALERPACIE_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI])
);


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[ALERPACIE].[IPCODPACI]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de la alerta del paciente: 1=Activo, 2=Inactivo. Indica si la advertencia sanitaria está vigente o desactivada.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ALERPACIE', @level2type = N'COLUMN', @level2name = N'ESTAALERT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado Alerta  1:Activo;  2:Inactivo;', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ALERPACIE', @level2type = N'COLUMN', @level2name = N'ESTAALERT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ALERPACIE', @level2type = N'COLUMN', @level2name = N'ESTAALERT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del usuario profesional de salud que inactivó o desactivó la alerta del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ALERPACIE', @level2type = N'COLUMN', @level2name = N'CODUSUINA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del usuario que Inactiva la Alerta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ALERPACIE', @level2type = N'COLUMN', @level2name = N'CODUSUINA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ALERPACIE', @level2type = N'COLUMN', @level2name = N'CODUSUINA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de inactivación o desactivación de la alerta del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ALERPACIE', @level2type = N'COLUMN', @level2name = N'FECHAINAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Inactivacion de la Alerta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ALERPACIE', @level2type = N'COLUMN', @level2name = N'FECHAINAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ALERPACIE', @level2type = N'COLUMN', @level2name = N'FECHAINAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del usuario profesional de salud que creó o registró la alerta del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ALERPACIE', @level2type = N'COLUMN', @level2name = N'CODUSUREG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del usuario que Creo la Alerta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ALERPACIE', @level2type = N'COLUMN', @level2name = N'CODUSUREG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ALERPACIE', @level2type = N'COLUMN', @level2name = N'CODUSUREG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación o registro de la alerta del paciente en el sistema.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ALERPACIE', @level2type = N'COLUMN', @level2name = N'FECHAREGI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Creacion de la Alerta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ALERPACIE', @level2type = N'COLUMN', @level2name = N'FECHAREGI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ALERPACIE', @level2type = N'COLUMN', @level2name = N'FECHAREGI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Comentario o descripción de la alerta: motivo, tipo de alergia, reacción adversa, medicamento, alérgeno o instrucción clínica para el paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ALERPACIE', @level2type = N'COLUMN', @level2name = N'COMENALER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Comentario Para la Alerta a Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ALERPACIE', @level2type = N'COLUMN', @level2name = N'COMENALER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ALERPACIE', @level2type = N'COLUMN', @level2name = N'COMENALER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único del paciente (cédula, documento de identidad, identificación PII). Referencia a paciente en sistema de atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ALERPACIE', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ALERPACIE', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ALERPACIE', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincrementable de cada registro de alerta del paciente en la tabla.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ALERPACIE', @level2type = N'COLUMN', @level2name = N'AUTOALERT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ALERPACIE', @level2type = N'COLUMN', @level2name = N'AUTOALERT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ALERPACIE', @level2type = N'COLUMN', @level2name = N'AUTOALERT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de alertas de alergias por paciente. Guarda las alergias conocidas de cada paciente, incluyendo observaciones, fechas de registro e inactivación, y estado de la alerta.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ALERPACIE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ALERPACIE';
