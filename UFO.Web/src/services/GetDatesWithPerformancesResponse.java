
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
 *         &lt;element name="GetDatesWithPerformancesResult" type="{http://andipaetzold.de/}ArrayOfDateTime" minOccurs="0"/>
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
    "getDatesWithPerformancesResult"
})
@XmlRootElement(name = "GetDatesWithPerformancesResponse")
public class GetDatesWithPerformancesResponse {

    @XmlElement(name = "GetDatesWithPerformancesResult")
    protected ArrayOfDateTime getDatesWithPerformancesResult;

    /**
     * Gets the value of the getDatesWithPerformancesResult property.
     * 
     * @return
     *     possible object is
     *     {@link ArrayOfDateTime }
     *     
     */
    public ArrayOfDateTime getGetDatesWithPerformancesResult() {
        return getDatesWithPerformancesResult;
    }

    /**
     * Sets the value of the getDatesWithPerformancesResult property.
     * 
     * @param value
     *     allowed object is
     *     {@link ArrayOfDateTime }
     *     
     */
    public void setGetDatesWithPerformancesResult(ArrayOfDateTime value) {
        this.getDatesWithPerformancesResult = value;
    }

}
