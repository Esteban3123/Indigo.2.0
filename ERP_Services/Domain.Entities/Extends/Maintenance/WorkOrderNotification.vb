Partial Public Class WorkOrderNotification

#Region "Methods"

    Public ReadOnly Property TypeName
        Get
            Dim description = String.Empty

            Select Case Me.Type
                Case 1
                    description = "Orden de Trabajo Asignada"
                Case 2
                    description = "Orden de Trabajo Terminada"
                Case 3
                    description = "Orden de Trabajo Anulada"
                Case 4
                    description = "Orden de Trabajo Aceptada"
                Case 5
                    description = "Orden de Trabajo Rechazada"
            End Select

            Return description
        End Get
    End Property

    Public ReadOnly Property Observation
        Get
            Dim description = String.Empty

            Select Case Me.Type
                Case 1
                    description = Me.WorkOrder.Description
                Case 2
                    description = Me.WorkOrder.Description
                Case 3
                    description = Me.WorkOrder.DescriptionReversal
                Case 4
                    description = Me.WorkOrder.DescriptionReversal
                Case 5
                    description = Me.WorkOrder.DescriptionReversal
            End Select

            Return description
        End Get
    End Property

    Public Function GetSubject() As String
        Return String.Format("{0}: {1} - Activo: {2} - {3}",
            Me.TypeName,
            Me.WorkOrder.Consecutive,
            Me.WorkOrder.Plate,
            Me.WorkOrder.PhysicalAssetDescription)
    End Function

    Public Function GetBody() As String
        Return String.Format("<html>
<body>
<div style='width:720px;margin:0 auto;padding:0;'>
    <table bgcolor='#EBEBEB' width='720' border='0' cellspacing='0' cellpadding='0' style=''>
        <tbody>            
			<tr>
				<td>&nbsp;</td>
				<td bgcolor='white'>
					<table width='100%' border='0' cellspacing='0' cellpadding='0' style='font-size:14px;text-align:justify;line-height:19px;'>
						<tbody>
							<tr>
								<td colspan='3'>&nbsp;</td>
							</tr>
							<tr>
								<td>&nbsp;</td>
								<td>{0}:
								</td>
								<td>&nbsp;</td>
							</tr>
							<tr>
								<td>&nbsp;</td>
								<td height='20'>&nbsp;</td>
								<td>&nbsp;</td>
							</tr>
							<tr>
								<td>&nbsp;</td>
								<td>
									<table width='100%' border='0' cellspacing='0' cellpadding='0' style='font-size:13px;font-family:Tahoma;'>
										<tbody>
											<tr>
												<td width='20%'>
													<strong>Código:</strong>
												</td>
												<td width='80%'>{1}</td>
											</tr>
											<tr>
												<td width='20%'>
													<strong>Fecha:</strong>
												</td>
												<td width='80%'>{2}</td>
											</tr>
                                            <tr>
												<td width='20%'>
													<strong>Artículo:</strong>
												</td>
												<td width='80%'>{3}</td>
											</tr>
											<tr>
												<td width='20%'>
													<strong>Placa:</strong>
												</td>
												<td width='80%'>{4}</td>
											</tr>
											<tr>
												<td width='20%'>
													<strong>Modelo:</strong>
												</td>
												<td width='80%'>{5}</td>
											</tr>
											<tr>
												<td width='20%'>
													<strong>Serial:</strong>
												</td>
												<td width='80%'>{6}</td>
											</tr>
											<tr>
												<td width='20%'>
													<strong>Ubicación:</strong>
												</td>
												<td width='80%'>{7}</td>
											</tr>
											<tr>
												<td width='20%'>
													<strong>Sucursal:</strong>
												</td>
												<td width='80%'>{8}</td>
											</tr>
											<tr>
												<td width='20%'>
													<strong>Responsable:</strong>
												</td>
												<td width='80%'>{9}</td>
											</tr>
											<tr>
												<td width='20%'>
													<strong>Teléfono:</strong>
												</td>
												<td width='80%'>{10}</td>
											</tr>
											<tr>
												<td width='20%'>
													<strong>Detalle:</strong>
												</td>
												<td width='80%'>{11}</td>
											</tr>
										</tbody>
									</table>
								</td>
								<td>&nbsp;</td>
							</tr>
							<tr>
								<td>&nbsp;</td>
								<td height='30'>&nbsp;</td>
								<td>&nbsp;</td>
							</tr>
							<tr>
								<td>&nbsp;</td>
								<td>
									<p style='margin:0;padding:0;'><b>{12}</b>.</p>
								</td>
								<td>&nbsp;</td>
							</tr>
							<tr>
								<td>&nbsp;</td>
								<td style='font-size:11px;line-height:13px;'>
									<p style='margin:0;padding:0;'>Departamento de Mantenimiento.</p>
								</td>
								<td>&nbsp;</td>
							</tr>
							<tr>
								<td>&nbsp;</td>
								<td height='30'></td>
								<td>&nbsp;</td>
							</tr>
							<tr>
								<td>&nbsp;</td>
								<td style='font-size:11px;line-height:13px;'>
									<p style='margin:0;padding:0;'>Nota: No responda este mensaje, ha sido enviado desde una dirección de correo electrónico no monitoreada.</p>
								</td>
								<td>&nbsp;</td>
							</tr>
							<tr>
								<td>&nbsp;</td>
								<td>&nbsp;</td>
								<td>&nbsp;</td>
							</tr>
						</tbody>
					</table>
				</td>
				<td>&nbsp;</td>
			</tr>
		</tbody>
	</table>
</div>
</body>
</html>",
            Me.TypeName,
            Me.WorkOrder.Consecutive,
            Me.WorkOrder.ProgramDate.ToString("yyyy-MM-dd"),
            Me.WorkOrder.PhysicalAssetDescription,
            Me.WorkOrder.Plate,
            Me.WorkOrder.Model,
            Me.WorkOrder.Serie,
            Me.WorkOrder.Location,
            Me.WorkOrder.BrachOfficeCodeName,
            Me.WorkOrder.MaintenanceResponsibleCodeName,
            Me.WorkOrder.ResponsiblePhone,
            Me.Observation,
            Me.WorkOrder.Company)
    End Function

#End Region

End Class
