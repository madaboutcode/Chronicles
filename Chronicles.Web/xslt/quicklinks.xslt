<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl"
>
  <xsl:output method="xml" indent="no"/>

  <xsl:template match="/">
    <xsl:apply-templates/>
  </xsl:template>
  <xsl:template match="item">
    <li>
      <a>
        <xsl:attribute name="href">
          <xsl:value-of select="link"/>
        </xsl:attribute>
        <xsl:attribute name="title">
          <xsl:value-of select="description"/>
        </xsl:attribute>
        <xsl:attribute name="alt">
          <xsl:value-of select="description"/>
        </xsl:attribute>
      [ <xsl:value-of select="title"/> ]
      </a>
    </li>
  </xsl:template>

</xsl:stylesheet>
