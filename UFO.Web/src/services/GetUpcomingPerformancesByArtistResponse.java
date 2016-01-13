
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
 *         &lt;element name="GetUpcomingPerformancesByArtistResult" type="{http://andipaetzold.de/}ArrayOfPerformance" minOccurs="0"/>
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
    "getUpcomingPerformancesByArtistResult"
})
@XmlRootElement(name = "GetUpcomingPerformancesByArtistResponse")
public class GetUpcomingPerformancesByArtistResponse {

    @XmlElement(name = "GetUpcomingPerformancesByArtistResult")
    protected ArrayOfPerformance getUpcomingPerformancesByArtistResult;

    /**
     * Gets the value of the getUpcomingPerformancesByArtistResult property.
     * 
     * @return
     *     possible object is
     *     {@link ArrayOfPerformance }
     *     
     */
    public ArrayOfPerformance getGetUpcomingPerformancesByArtistResult() {
        return getUpcomingPerformancesByArtistResult;
    }

    /**
     * Sets the value of the getUpcomingPerformancesByArtistResult property.
     * 
     * @param value
     *     allowed object is
     *     {@link ArrayOfPerformance }
     *     
     */
    public void setGetUpcomingPerformancesByArtistResult(ArrayOfPerformance value) {
        this.getUpcomingPerformancesByArtistResult = value;
    }

}
