
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
 *         &lt;element name="GetAllButDeletedArtistsResult" type="{http://andipaetzold.de/}ArrayOfArtist" minOccurs="0"/>
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
    "getAllButDeletedArtistsResult"
})
@XmlRootElement(name = "GetAllButDeletedArtistsResponse")
public class GetAllButDeletedArtistsResponse {

    @XmlElement(name = "GetAllButDeletedArtistsResult")
    protected ArrayOfArtist getAllButDeletedArtistsResult;

    /**
     * Gets the value of the getAllButDeletedArtistsResult property.
     * 
     * @return
     *     possible object is
     *     {@link ArrayOfArtist }
     *     
     */
    public ArrayOfArtist getGetAllButDeletedArtistsResult() {
        return getAllButDeletedArtistsResult;
    }

    /**
     * Sets the value of the getAllButDeletedArtistsResult property.
     * 
     * @param value
     *     allowed object is
     *     {@link ArrayOfArtist }
     *     
     */
    public void setGetAllButDeletedArtistsResult(ArrayOfArtist value) {
        this.getAllButDeletedArtistsResult = value;
    }

}
