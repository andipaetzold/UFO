
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
 *         &lt;element name="GetAllPerformancesResult" type="{http://andipaetzold.de/}ArrayOfPerformance" minOccurs="0"/>
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
    "getAllPerformancesResult"
})
@XmlRootElement(name = "GetAllPerformancesResponse")
public class GetAllPerformancesResponse {

    @XmlElement(name = "GetAllPerformancesResult")
    protected ArrayOfPerformance getAllPerformancesResult;

    /**
     * Gets the value of the getAllPerformancesResult property.
     * 
     * @return
     *     possible object is
     *     {@link ArrayOfPerformance }
     *     
     */
    public ArrayOfPerformance getGetAllPerformancesResult() {
        return getAllPerformancesResult;
    }

    /**
     * Sets the value of the getAllPerformancesResult property.
     * 
     * @param value
     *     allowed object is
     *     {@link ArrayOfPerformance }
     *     
     */
    public void setGetAllPerformancesResult(ArrayOfPerformance value) {
        this.getAllPerformancesResult = value;
    }

}
