Partial Public Class MaintenanceFailureRequestDetailNotification

#Region "Methods"

    Public Function GetSubject() As String
        Return String.Format("Solicitud de Mantenimiento: {0} - Activo: {1} - {2}",
            Me.MaintenanceFailureRequestDetail.MaintenanceFailureRequest.Code,
            Me.MaintenanceFailureRequestDetail.Plate,
            Me.MaintenanceFailureRequestDetail.NameArticle)
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
								<td>Se ha procesado un reporte de Falla:
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
												<td width='80%'>{0}</td>
											</tr>
											<tr>
												<td width='20%'>
													<strong>Fecha:</strong>
												</td>
												<td width='80%'>{1}</td>
											</tr>
                                            <tr>
												<td width='20%'>
													<strong>Artículo:</strong>
												</td>
												<td width='80%'>{2}</td>
											</tr>
											<tr>
												<td width='20%'>
													<strong>Placa:</strong>
												</td>
												<td width='80%'>{3}</td>
											</tr>
											<tr>
												<td width='20%'>
													<strong>Modelo:</strong>
												</td>
												<td width='80%'>{4}</td>
											</tr>
											<tr>
												<td width='20%'>
													<strong>Serial:</strong>
												</td>
												<td width='80%'>{5}</td>
											</tr>
											<tr>
												<td width='20%'>
													<strong>Ubicación:</strong>
												</td>
												<td width='80%'>{6}</td>
											</tr>
											<tr>
												<td width='20%'>
													<strong>Sucursal:</strong>
												</td>
												<td width='80%'>{7}</td>
											</tr>
											<tr>
												<td width='20%'>
													<strong>Responsable:</strong>
												</td>
												<td width='80%'>{8}</td>
											</tr>
											<tr>
												<td width='20%'>
													<strong>Teléfono:</strong>
												</td>
												<td width='80%'>{9}</td>
											</tr>
											<tr>
												<td width='20%'>
													<strong>Detalle:</strong>
												</td>
												<td width='80%'>{10}</td>
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
									<p style='margin:0;padding:0;'><b>{11}</b>.</p>
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
            Me.MaintenanceFailureRequestDetail.MaintenanceFailureRequest.Code,
            Me.MaintenanceFailureRequestDetail.MaintenanceFailureRequest.DateFailure.ToString("yyyy-MM-dd"),
            Me.MaintenanceFailureRequestDetail.NameArticle,
            Me.MaintenanceFailureRequestDetail.Plate,
            Me.MaintenanceFailureRequestDetail.Model,
            Me.MaintenanceFailureRequestDetail.Serie,
            Me.MaintenanceFailureRequestDetail.Location,
            Me.MaintenanceFailureRequestDetail.MaintenanceFailureRequest.BranchOfficeCodeName,
            Me.MaintenanceFailureRequestDetail.Responsible,
            Me.MaintenanceFailureRequestDetail.ResponsiblePhone,
            Me.MaintenanceFailureRequestDetail.Description,
            Me.MaintenanceFailureRequestDetail.MaintenanceFailureRequest.Company)
    End Function

#End Region

End Class
