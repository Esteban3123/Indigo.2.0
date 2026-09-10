CREATE TABLE [dbo].[RIASCUPSPACIENTE] (
    [ID]                     INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IPCODPACI]              VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [IDRIASCUPS]             INT                                                                              NOT NULL,
    [CODSERIPS]              CHAR (20)                                                                        NOT NULL,
    [CANTIDAD]               INT                                                                              NOT NULL,
    [ESTADO]                 INT                                                                              NOT NULL,
    [FECHAMINREALIZAR]       DATETIME                                                                         NOT NULL,
    [FECHAMAXREALIZAR]       DATETIME                                                                         NOT NULL,
    [IDRIASCUPSD]            INT                                                                              NOT NULL,
    [ORIGEN]                 INT                                                                              NOT NULL,
    [ACCION]                 INT                                                                              NOT NULL,
    [FOLIO]                  CHAR (10)                                                                        NULL,
    [NUMINGRES]              CHAR (10)                                                                        NULL,
    [FECHAREALIZACION]       DATETIME                                                                         NULL,
    [IDCITA]                 INT                                                                              NULL,
    [IDDETALLEORDENSERVICIO] INT                                                                              NULL,
    [IDFACTURA]              INT                                                                              NULL,
    [VALORCUPS]              DECIMAL (18, 2)                                                                  NULL,
    [CODPROSAL]              CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NULL,
    [CODUSUARU]              CHAR (20)                                                                        NOT NULL,
    [FECHACREACION]          DATETIME                                                                         NOT NULL,
    [CODUSUARUMOD]           CHAR (20)                                                                        NULL,
    [FECHAMODIFICACION]      DATETIME                                                                         NULL,
    CONSTRAINT [PK_RIASCUPSPACIENTE] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_RIASCUPSPACIENTE_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_RIASCUPSPACIENTE_INCUPSIPS] FOREIGN KEY ([CODSERIPS]) REFERENCES [dbo].[INCUPSIPS] ([CODSERIPS]),
    CONSTRAINT [FK_RIASCUPSPACIENTE_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_RIASCUPSPACIENTE_INPROFSAL] FOREIGN KEY ([CODPROSAL]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_RIASCUPSPACIENTE_RIASCUPS] FOREIGN KEY ([IDRIASCUPS]) REFERENCES [dbo].[RIASCUPS] ([ID]),
    CONSTRAINT [FK_RIASCUPSPACIENTE_RIASCUPSD] FOREIGN KEY ([IDRIASCUPSD]) REFERENCES [dbo].[RIASCUPSD] ([ID])
);


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[RIASCUPSPACIENTE].[IPCODPACI]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[RIASCUPSPACIENTE].[CODPROSAL]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');




GO
CREATE NONCLUSTERED INDEX [IX_RIASCUPSPACIENTE__IPCODPACI__IDRIASCUPS__CODSERIPS__ESTADO__INC__ID__CANTIDAD__FECHAREALIZACION]
    ON [dbo].[RIASCUPSPACIENTE]([IPCODPACI] ASC, [IDRIASCUPS] ASC, [CODSERIPS] ASC, [ESTADO] ASC)
    INCLUDE([CANTIDAD], [FECHAREALIZACION], [ID]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora del último cambio realizado al registro CUPS-Paciente (DATETIME, NULL si no fue modificado)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASCUPSPACIENTE', @level2type = N'COLUMN', @level2name = N'FECHAMODIFICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda la fecha de la modificacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASCUPSPACIENTE', @level2type = N'COLUMN', @level2name = N'FECHAMODIFICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASCUPSPACIENTE', @level2type = N'COLUMN', @level2name = N'FECHAMODIFICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del usuario que realizó la última modificación del registro (CHAR 20, PII ofuscado, FK INPROFSAL)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASCUPSPACIENTE', @level2type = N'COLUMN', @level2name = N'CODUSUARUMOD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el codigo de usuario quien modifica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASCUPSPACIENTE', @level2type = N'COLUMN', @level2name = N'CODUSUARUMOD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASCUPSPACIENTE', @level2type = N'COLUMN', @level2name = N'CODUSUARUMOD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación inicial del registro de CUPS-Paciente en el sistema (DATETIME)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASCUPSPACIENTE', @level2type = N'COLUMN', @level2name = N'FECHACREACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda la fecha de creacion ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASCUPSPACIENTE', @level2type = N'COLUMN', @level2name = N'FECHACREACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASCUPSPACIENTE', @level2type = N'COLUMN', @level2name = N'FECHACREACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del usuario/profesional que generó la orden de servicio CUPS en VIE ERP (CHAR 20, requerido)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASCUPSPACIENTE', @level2type = N'COLUMN', @level2name = N'CODUSUARU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Usuario que genero la orden de servicio VIE ERP', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASCUPSPACIENTE', @level2type = N'COLUMN', @level2name = N'CODUSUARU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASCUPSPACIENTE', @level2type = N'COLUMN', @level2name = N'CODUSUARU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del profesional de la salud asignado a la orden de servicio CUPS (CHAR 20, PII ofuscado, FK INPROFSAL, puede ser NULL)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASCUPSPACIENTE', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Profesional que se coloco en la orden  de servicio generada de VIE ERP', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASCUPSPACIENTE', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASCUPSPACIENTE', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor monetario unitario del código CUPS en la orden de servicio (DECIMAL 18,2, COP, puede ser NULL)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASCUPSPACIENTE', @level2type = N'COLUMN', @level2name = N'VALORCUPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor del cups', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASCUPSPACIENTE', @level2type = N'COLUMN', @level2name = N'VALORCUPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASCUPSPACIENTE', @level2type = N'COLUMN', @level2name = N'VALORCUPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la factura asociada a la prestación CUPS (INT, FK ADFACTUR, puede ser NULL)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASCUPSPACIENTE', @level2type = N'COLUMN', @level2name = N'IDFACTURA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la factura de VIE ERP', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASCUPSPACIENTE', @level2type = N'COLUMN', @level2name = N'IDFACTURA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASCUPSPACIENTE', @level2type = N'COLUMN', @level2name = N'IDFACTURA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del detalle de orden de servicios que originó el CUPS (INT, puede ser NULL)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASCUPSPACIENTE', @level2type = N'COLUMN', @level2name = N'IDDETALLEORDENSERVICIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID detalle de la orden de servicios de vie ERP', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASCUPSPACIENTE', @level2type = N'COLUMN', @level2name = N'IDDETALLEORDENSERVICIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASCUPSPACIENTE', @level2type = N'COLUMN', @level2name = N'IDDETALLEORDENSERVICIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la cita médica agendada para realizar el CUPS (INT, FK AGASICITA, puede ser NULL)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASCUPSPACIENTE', @level2type = N'COLUMN', @level2name = N'IDCITA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cita (AGASICITA)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASCUPSPACIENTE', @level2type = N'COLUMN', @level2name = N'IDCITA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASCUPSPACIENTE', @level2type = N'COLUMN', @level2name = N'IDCITA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora en que efectivamente se ejecutó/realizó el procedimiento CUPS (DATETIME, puede ser NULL)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASCUPSPACIENTE', @level2type = N'COLUMN', @level2name = N'FECHAREALIZACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha en que se realizo CUPS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASCUPSPACIENTE', @level2type = N'COLUMN', @level2name = N'FECHAREALIZACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASCUPSPACIENTE', @level2type = N'COLUMN', @level2name = N'FECHAREALIZACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de ingreso/admisión con el que se generó la orden de servicio CUPS (CHAR 10, FK ADINGRESO, puede ser NULL)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASCUPSPACIENTE', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Ingreso con el que se genero la orden de servicio VIE ERP', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASCUPSPACIENTE', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASCUPSPACIENTE', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Folio de la historia clínica que solicitó el CUPS, requerido solo cuando el procedimiento exige orden médica previa (CHAR 10, puede ser NULL)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASCUPSPACIENTE', @level2type = N'COLUMN', @level2name = N'FOLIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Folio de la HC que solicito, solo cuando el CUPS requiere orden medica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASCUPSPACIENTE', @level2type = N'COLUMN', @level2name = N'FOLIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASCUPSPACIENTE', @level2type = N'COLUMN', @level2name = N'FOLIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de última acción registrada: 1=Inserción, 2=Actualización (INT, indicador de auditoría)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASCUPSPACIENTE', @level2type = N'COLUMN', @level2name = N'ACCION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'1 - si la ultima accion del registro fue una Insercion (insert)  2 - si la ultima accion fue una actualizacion (update)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASCUPSPACIENTE', @level2type = N'COLUMN', @level2name = N'ACCION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASCUPSPACIENTE', @level2type = N'COLUMN', @level2name = N'ACCION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Proceso que generó el registro: 1=Dashboard RIAS, 2=Orden Médica, 3=Cita Médica, 4=Orden de Servicio (INT, clasificador de fuente)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASCUPSPACIENTE', @level2type = N'COLUMN', @level2name = N'ORIGEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Determina el proceso de donde se genero la inscripcion   1 - Dashboard Rias  2 - Orden Medica  3 - Cita Medica  4 - Orden Servicio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASCUPSPACIENTE', @level2type = N'COLUMN', @level2name = N'ORIGEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASCUPSPACIENTE', @level2type = N'COLUMN', @level2name = N'ORIGEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de relación con el rango/detalle RIASCUPSD que generó este registro (INT, FK RIASCUPSD, requerido)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASCUPSPACIENTE', @level2type = N'COLUMN', @level2name = N'IDRIASCUPSD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de relacion con el rango que me genero el registro (RIASCUPSD)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASCUPSPACIENTE', @level2type = N'COLUMN', @level2name = N'IDRIASCUPSD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASCUPSPACIENTE', @level2type = N'COLUMN', @level2name = N'IDRIASCUPSD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha máxima permitida para ejecutar el procedimiento CUPS según normativa (DATETIME, requerido)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASCUPSPACIENTE', @level2type = N'COLUMN', @level2name = N'FECHAMAXREALIZAR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha maxima para realizar el cups', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASCUPSPACIENTE', @level2type = N'COLUMN', @level2name = N'FECHAMAXREALIZAR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASCUPSPACIENTE', @level2type = N'COLUMN', @level2name = N'FECHAMAXREALIZAR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha mínima a partir de la cual puede ejecutarse el procedimiento CUPS (DATETIME, requerido)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASCUPSPACIENTE', @level2type = N'COLUMN', @level2name = N'FECHAMINREALIZAR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha minima para realizar el cups', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASCUPSPACIENTE', @level2type = N'COLUMN', @level2name = N'FECHAMINREALIZAR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASCUPSPACIENTE', @level2type = N'COLUMN', @level2name = N'FECHAMINREALIZAR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del CUPS: 1=Sin realizar, 2=Realizado (INT, indicador de ejecución)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASCUPSPACIENTE', @level2type = N'COLUMN', @level2name = N'ESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'1 -Sin realizar  2 - Realizado  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASCUPSPACIENTE', @level2type = N'COLUMN', @level2name = N'ESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASCUPSPACIENTE', @level2type = N'COLUMN', @level2name = N'ESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de prestaciones CUPS solicitadas/autorizadas (INT, requerido)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASCUPSPACIENTE', @level2type = N'COLUMN', @level2name = N'CANTIDAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'cantidad solicitada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASCUPSPACIENTE', @level2type = N'COLUMN', @level2name = N'CANTIDAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASCUPSPACIENTE', @level2type = N'COLUMN', @level2name = N'CANTIDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del procedimiento CUPS según catálogo INCUPSIPS (CHAR 20, FK INCUPSIPS, requerido)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASCUPSPACIENTE', @level2type = N'COLUMN', @level2name = N'CODSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de CUPS ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASCUPSPACIENTE', @level2type = N'COLUMN', @level2name = N'CODSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASCUPSPACIENTE', @level2type = N'COLUMN', @level2name = N'CODSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la solicitud RIAS padre que agrupa este CUPS (INT, FK RIASCUPS, requerido)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASCUPSPACIENTE', @level2type = N'COLUMN', @level2name = N'IDRIASCUPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de la RIAS (Tabla RIAS)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASCUPSPACIENTE', @level2type = N'COLUMN', @level2name = N'IDRIASCUPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASCUPSPACIENTE', @level2type = N'COLUMN', @level2name = N'IDRIASCUPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificación del paciente (cédula, pasaporte, documento, equivalente IPCODPACI) (VARCHAR 25, PII ofuscado, FK INPACIENT, requerido)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASCUPSPACIENTE', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificacion del paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASCUPSPACIENTE', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASCUPSPACIENTE', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincrementado del registro CUPS-Paciente (INT IDENTITY, clave primaria)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASCUPSPACIENTE', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASCUPSPACIENTE', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASCUPSPACIENTE', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de servicios CUPS asignados a pacientes dentro de un plan RIAS (Rutas Integrales de Atención en Salud). Controla qué procedimientos o exámenes debe realizar cada paciente, en qué fechas y en qué estado se encuentran, incluyendo su realización, facturación y profesional responsable.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASCUPSPACIENTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASCUPSPACIENTE';

GO
CREATE NONCLUSTERED INDEX [IX_RIASCUPSPACIENTE_Composite]
    ON [dbo].[RIASCUPSPACIENTE]([IPCODPACI] ASC, [IDDETALLEORDENSERVICIO] ASC, [IDRIASCUPS] ASC, [CODSERIPS] ASC);
