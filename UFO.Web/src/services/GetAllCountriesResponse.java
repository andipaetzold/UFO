
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
 *         &lt;element name="GetAllCountriesResult" type="{http://andipaetzold.de/}ArrayOfCountry" minOccurs="0"/>
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
    "getAllCountriesResult"
})
@XmlRootElement(name = "GetAllCountriesResponse")
public class GetAllCountriesResponse {

    @XmlElement(name = "GetAllCountriesResult")
    protected ArrayOfCountry getAllCountriesResult;

    /**
     * Gets the value of the getAllCountriesResult property.
     * 
     * @return
     *     possible object is
     *     {@link ArrayOfCountry }
     *     
     */
    public ArrayOfCountry getGetAllCountriesResult() {
        return getAllCountriesResult;
    }

    /**
     * Sets the value of the getAllCountriesResult property.
     * 
     * @param value
     *     allowed object is
     *     {@link ArrayOfCountry }
     *     
     */
    public void setGetAllCountriesResult(ArrayOfCountry value) {
        this.getAllCountriesResult = value;
    }

}
