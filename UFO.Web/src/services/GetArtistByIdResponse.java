
package services;

import javax.xml.bind.annotation.XmlAccessType;
import javax.xml.bind.annotation.XmlAccessorType;
import javax.xml.bind.annotation.XmlElement;
import javax.xml.bind.annotation.XmlRootElement;
import javax.xml.bind.annotation.XmlType;


/**
 * <p>Java class for anonymous complex type.
 * 
 * <p>The following schema fragment specifies the expected content contained within this class.
 * 
 * <pre>
 * &lt;complexType>
 *   &lt;complexContent>
 *     &lt;restriction base="{http://www.w3.org/2001/XMLSchema}anyType">
 *       &lt;sequence>
 *         &lt;element name="GetArtistByIdResult" type="{http://andipaetzold.de/}Artist" minOccurs="0"/>
 *       &lt;/sequence>
 *     &lt;/restriction>
 *   &lt;/complexContent>
 * &lt;/complexType>
 * </pre>
 * 
 * 
 */
@XmlAccessorType(XmlAccessType.FIELD)
@XmlType(name = "", propOrder = {
    "getArtistByIdResult"
})
@XmlRootElement(name = "GetArtistByIdResponse")
public class GetArtistByIdResponse {

    @XmlElement(name = "GetArtistByIdResult")
    protected Artist getArtistByIdResult;

    /**
     * Gets the value of the getArtistByIdResult property.
     * 
     * @return
     *     possible object is
     *     {@link Artist }
     *     
     */
    public Artist getGetArtistByIdResult() {
        return getArtistByIdResult;
    }

    /**
     * Sets the value of the getArtistByIdResult property.
     * 
     * @param value
     *     allowed object is
     *     {@link Artist }
     *     
     */
    public void setGetArtistByIdResult(Artist value) {
        this.getArtistByIdResult = value;
    }

}
