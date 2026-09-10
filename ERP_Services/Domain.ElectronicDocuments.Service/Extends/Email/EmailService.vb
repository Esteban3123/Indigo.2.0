Public Class EmailService

    Public Shared Function GetFormat() As String
        Return "<html>
<body>
<div style='width:620px;margin:0 auto;padding:0;'>
    <table bgcolor='#EBEBEB' width='620' border='0' cellspacing='0' cellpadding='0' style=''>
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
								<td width='6%'>&nbsp;</td>
								<td width='88%' style='font-size:16px;font-weight:bold;'>{0} - {1},</td>
								<td width='6%'>&nbsp;</td>
							</tr>
							<tr>
								<td>&nbsp;</td>
								<td>&nbsp;</td>
								<td>&nbsp;</td>
							</tr>
							<tr>
								<td>&nbsp;</td>
								<td>Ha recibido un documento electrónico generado y enviado mediante el sistema de Facturación Electrónica del Software con la siguiente información:
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
								<td height='20'>
									<strong style='color:#1F52A5;font-size:13px;font-family:Tahoma;'>Datos del Emisor</strong>
								</td>
								<td>&nbsp;</td>
							</tr>
							<tr>
								<td>&nbsp;</td>
								<td>
									<table width='100%' border='0' cellspacing='0' cellpadding='0' style='font-size:13px;font-family:Tahoma;'>
										<tbody>
											<tr>
												<td width='20%'>
													<strong>Empresa:</strong>
												</td>
												<td width='80%'>{2}</td>
											</tr>
											<tr>
												<td width='20%'>
													<strong>Identificación:</strong>
												</td>
												<td width='80%'>{3}</td>
											</tr>
										</tbody>
									</table>
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
								<td height='20'>
									<strong style='color:#1F52A5;font-size:13px;font-family:Tahoma;'>Información del Documento</strong>
								</td>
								<td>&nbsp;</td>
							</tr>
							<tr>
								<td>&nbsp;</td>
								<td>
									<table width='100%' border='0' cellspacing='0' cellpadding='0' style='font-size:13px;font-family:Tahoma;'>
										<tbody>
											<tr>
												<td width='20%'>
													<strong>Fecha:</strong>
												</td>
												<td width='80%'>{4}</td>
											</tr>
											<tr>
												<td width='20%'>
													<strong>Tipo: </strong>
												</td>
												<td width='80%'>{5}</td>
											</tr>
											<tr>
												<td width='20%'>
													<strong>Número:</strong>
												</td>
												<td width='80%'>{6}</td>
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
									<p style='margin:0;padding:0;'>Adjunto encontrará el documento electrónico en formato <b>XML</b>.</p>
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
</html>"
    End Function

End Class
