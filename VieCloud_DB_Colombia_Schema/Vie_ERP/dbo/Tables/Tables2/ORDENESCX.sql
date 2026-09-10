CREATE TABLE [dbo].[ORDENESCX] (
    [IDETIPHIS]         CHAR (9)                                                                         NOT NULL,
    [NUMEFOLIO]         NCHAR (10)                                                                       NOT NULL,
    [IPCODPACI]         VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES]         CHAR (10)                                                                        NOT NULL,
    [CODCENATE]         CHAR (10)                                                                        NOT NULL,
    [UFUCODIGO]         CHAR (10)                                                                        NOT NULL,
    [CODPROSAL]         CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NOT NULL,
    [FECORDMED]         DATETIME                                                                         NOT NULL,
    [CODSERIPS]         CHAR (20)                                                                        NOT NULL,
    [CANSERIPS]         INT                                                                              NOT NULL,
    [OBSSERIPS]         CHAR (250)                                                                       NULL,
    [PRISERIPS]         CHAR (1)                                                                         NULL,
    [ESTSERIPS]         CHAR (1)                                                                         NULL,
    [MANEXTPRO]         BIT                                                                              NULL,
    [CODDIAGNO]         CHAR (4) MASKED WITH (FUNCTION = 'partial(0, "DiagnosticCode_Ofuscado", 0)')     NULL,
    [INTERPRET]         VARCHAR (4000)                                                                   NULL,
    [CODPROINT]         CHAR (20)                                                                        NULL,
    [NUMFOLINT]         NCHAR (10)                                                                       NULL,
    [SERREAINT]         BIT                                                                              NULL,
    [INDAUDFOR]         NUMERIC (18)                                                                     NULL,
    [ESTALELAB]         BIT                                                                              NULL,
    [FECRECMUE]         DATETIME                                                                         NULL,
    [NOMARCLAB]         CHAR (250)                                                                       NULL,
    [USURECMUE]         CHAR (20)                                                                        NULL,
    [AUTO]              INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IpsAutorizada]     NCHAR (10)                                                                       NULL,
    [Observacion]       NVARCHAR (200)                                                                   NULL,
    [Estado]            INT                                                                              NULL,
    [Radicado]          NCHAR (10)                                                                       NULL,
    [FechaVencimiento]  DATETIME                                                                         NULL,
    [FechaRegQx]        DATETIME                                                                         NULL,
    [FechaCirugia]      DATETIME                                                                         NULL,
    [FechaAnestesia]    DATETIME                                                                         NULL,
    [CirugiaRealizada]  CHAR (10)                                                                        NULL,
    [FechaAutorizacion] DATETIME                                                                         NULL,
    [VistoAnestesia]    CHAR (10)                                                                        NULL,
    [ObservAnestesia]   NVARCHAR (200)                                                                   NULL,
    [CONCURRE]          ROWVERSION                                                                       NULL,
    CONSTRAINT [PK_ORDENESCX__NUMEFOLIO__IPCODPACI__NUMINGRES__CODSERIPS] PRIMARY KEY CLUSTERED ([NUMEFOLIO] ASC, [IPCODPACI] ASC, [NUMINGRES] ASC, [CODSERIPS] ASC),
    CONSTRAINT [FK_ORDENESCX_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_ORDENESCX_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI])
);


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[ORDENESCX].[IPCODPACI]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[ORDENESCX].[CODPROSAL]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[ORDENESCX].[CODDIAGNO]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');




GO



GO


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Órdenes de cirugía (CX) generadas en la historia clínica del paciente. Registra cada procedimiento quirúrgico ordenado, su estado, fechas clave del proceso quirúrgico y de anestesia, autorización de la IPS y seguimiento de laboratorio o toma de muestras.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ORDENESCX';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ORDENESCX';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de identificación del paciente (cédula, tarjeta de identidad, pasaporte, etc.).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ORDENESCX', @level2type = N'COLUMN', @level2name = N'IDETIPHIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ORDENESCX', @level2type = N'COLUMN', @level2name = N'IDETIPHIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de folio o consecutivo de la orden quirúrgica dentro de la historia clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ORDENESCX', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ORDENESCX', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cédula o documento de identidad del paciente (identificación ofuscada por seguridad).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ORDENESCX', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ORDENESCX', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de ingreso o admisión del paciente al que pertenece la orden de cirugía.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ORDENESCX', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ORDENESCX', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del centro de atención donde se generó la orden quirúrgica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ORDENESCX', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ORDENESCX', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la unidad funcional (servicio o área) que origina la orden de cirugía.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ORDENESCX', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ORDENESCX', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código o identificación del profesional de la salud que ordena la cirugía (ofuscado por seguridad).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ORDENESCX', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ORDENESCX', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora en que se generó la orden médica de cirugía.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ORDENESCX', @level2type = N'COLUMN', @level2name = N'FECORDMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ORDENESCX', @level2type = N'COLUMN', @level2name = N'FECORDMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del procedimiento quirúrgico ordenado (CUPS/RIPS), identificador del servicio.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ORDENESCX', @level2type = N'COLUMN', @level2name = N'CODSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ORDENESCX', @level2type = N'COLUMN', @level2name = N'CODSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad o número de veces que se ordena el procedimiento quirúrgico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ORDENESCX', @level2type = N'COLUMN', @level2name = N'CANSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ORDENESCX', @level2type = N'COLUMN', @level2name = N'CANSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observaciones o indicaciones clínicas asociadas al procedimiento quirúrgico ordenado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ORDENESCX', @level2type = N'COLUMN', @level2name = N'OBSSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ORDENESCX', @level2type = N'COLUMN', @level2name = N'OBSSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Prioridad del procedimiento quirúrgico (urgente, electivo, etc.).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ORDENESCX', @level2type = N'COLUMN', @level2name = N'PRISERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ORDENESCX', @level2type = N'COLUMN', @level2name = N'PRISERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del servicio o procedimiento en la orden (pendiente, realizado, cancelado, etc.).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ORDENESCX', @level2type = N'COLUMN', @level2name = N'ESTSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ORDENESCX', @level2type = N'COLUMN', @level2name = N'ESTSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica si el procedimiento se maneja con proveedor externo (IPS externa o proveedor tercerizado).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ORDENESCX', @level2type = N'COLUMN', @level2name = N'MANEXTPRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ORDENESCX', @level2type = N'COLUMN', @level2name = N'MANEXTPRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de diagnóstico CIE-10 relacionado con la indicación quirúrgica (ofuscado por seguridad).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ORDENESCX', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ORDENESCX', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Interpretación clínica o reporte del profesional sobre el procedimiento o resultado quirúrgico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ORDENESCX', @level2type = N'COLUMN', @level2name = N'INTERPRET';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ORDENESCX', @level2type = N'COLUMN', @level2name = N'INTERPRET';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del profesional de salud que realiza la interpretación o el procedimiento interno.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ORDENESCX', @level2type = N'COLUMN', @level2name = N'CODPROINT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ORDENESCX', @level2type = N'COLUMN', @level2name = N'CODPROINT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de folio del procedimiento o interpretación interna asociada a la orden.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ORDENESCX', @level2type = N'COLUMN', @level2name = N'NUMFOLINT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ORDENESCX', @level2type = N'COLUMN', @level2name = N'NUMFOLINT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica si el servicio fue realizado internamente en la institución.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ORDENESCX', @level2type = N'COLUMN', @level2name = N'SERREAINT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ORDENESCX', @level2type = N'COLUMN', @level2name = N'SERREAINT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador o referencia de auditoría formal asociada a la orden quirúrgica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ORDENESCX', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ORDENESCX', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica si la orden tiene una alerta de laboratorio activa relacionada con la cirugía.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ORDENESCX', @level2type = N'COLUMN', @level2name = N'ESTALELAB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ORDENESCX', @level2type = N'COLUMN', @level2name = N'ESTALELAB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de recepción de muestra en el laboratorio para estudios preoperatorios.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ORDENESCX', @level2type = N'COLUMN', @level2name = N'FECRECMUE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ORDENESCX', @level2type = N'COLUMN', @level2name = N'FECRECMUE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre o marca del laboratorio que procesa las muestras asociadas a la orden.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ORDENESCX', @level2type = N'COLUMN', @level2name = N'NOMARCLAB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ORDENESCX', @level2type = N'COLUMN', @level2name = N'NOMARCLAB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario o funcionario que registró la recepción de la muestra en el laboratorio.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ORDENESCX', @level2type = N'COLUMN', @level2name = N'USURECMUE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ORDENESCX', @level2type = N'COLUMN', @level2name = N'USURECMUE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincremental interno de la orden de cirugía.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ORDENESCX', @level2type = N'COLUMN', @level2name = N'AUTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ORDENESCX', @level2type = N'COLUMN', @level2name = N'AUTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la IPS autorizada para realizar el procedimiento quirúrgico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ORDENESCX', @level2type = N'COLUMN', @level2name = N'IpsAutorizada';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ORDENESCX', @level2type = N'COLUMN', @level2name = N'IpsAutorizada';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observaciones adicionales sobre la orden de cirugía, autorización o proceso.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ORDENESCX', @level2type = N'COLUMN', @level2name = N'Observacion';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ORDENESCX', @level2type = N'COLUMN', @level2name = N'Observacion';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado general de la orden quirúrgica (activa, anulada, autorizada, ejecutada, etc.).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ORDENESCX', @level2type = N'COLUMN', @level2name = N'Estado';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ORDENESCX', @level2type = N'COLUMN', @level2name = N'Estado';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de radicado de la solicitud de autorización ante el asegurador o EPS.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ORDENESCX', @level2type = N'COLUMN', @level2name = N'Radicado';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ORDENESCX', @level2type = N'COLUMN', @level2name = N'Radicado';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de vencimiento de la autorización para realizar la cirugía.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ORDENESCX', @level2type = N'COLUMN', @level2name = N'FechaVencimiento';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ORDENESCX', @level2type = N'COLUMN', @level2name = N'FechaVencimiento';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de registro de la orden o programación quirúrgica en el sistema.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ORDENESCX', @level2type = N'COLUMN', @level2name = N'FechaRegQx';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ORDENESCX', @level2type = N'COLUMN', @level2name = N'FechaRegQx';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha programada o realizada de la cirugía.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ORDENESCX', @level2type = N'COLUMN', @level2name = N'FechaCirugia';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ORDENESCX', @level2type = N'COLUMN', @level2name = N'FechaCirugia';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha en que se realiza la valoración o procedimiento de anestesia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ORDENESCX', @level2type = N'COLUMN', @level2name = N'FechaAnestesia';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ORDENESCX', @level2type = N'COLUMN', @level2name = N'FechaAnestesia';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica si la cirugía fue efectivamente realizada (sí/no u otro código de confirmación).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ORDENESCX', @level2type = N'COLUMN', @level2name = N'CirugiaRealizada';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ORDENESCX', @level2type = N'COLUMN', @level2name = N'CirugiaRealizada';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha en que el asegurador o EPS autorizó el procedimiento quirúrgico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ORDENESCX', @level2type = N'COLUMN', @level2name = N'FechaAutorizacion';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ORDENESCX', @level2type = N'COLUMN', @level2name = N'FechaAutorizacion';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica si el anestesiólogo revisó y aprobó la orden o valoración prequirúrgica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ORDENESCX', @level2type = N'COLUMN', @level2name = N'VistoAnestesia';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ORDENESCX', @level2type = N'COLUMN', @level2name = N'VistoAnestesia';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observaciones del anestesiólogo sobre el paciente o el procedimiento anestésico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ORDENESCX', @level2type = N'COLUMN', @level2name = N'ObservAnestesia';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ORDENESCX', @level2type = N'COLUMN', @level2name = N'ObservAnestesia';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca de tiempo (timestamp) para control de concurrencia y auditoría de cambios en el registro.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ORDENESCX', @level2type = N'COLUMN', @level2name = N'CONCURRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ORDENESCX', @level2type = N'COLUMN', @level2name = N'CONCURRE';
